using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;
using MercadoPago.Config;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using System.Net;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class VentaController : Controller
    {
        private readonly SistemaContext _context;
        private readonly UserManager<Usuario> _userManager;

        public VentaController(SistemaContext context, UserManager<Usuario> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            MercadoPagoConfig.AccessToken = configuration["MercadoPago:AccessToken"];
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Index", "Producto");

            var pedido = await ObtenerPedidoAsync(null, user.Id);
            if (pedido == null) return RedirectToAction("Index", "Producto");

            var venta = new Venta
            {
                Fecha = DateTime.Now,
                PedidoId = pedido.Id,
                Pedido = pedido,
                MetodoPago = "Tarjeta"
            };

            return View(venta);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Venta venta, string pago)
        {
            var pedido = await ObtenerPedidoAsync(venta.PedidoId, null);
            if (pedido == null) return RedirectToAction("Index", "Producto");

            venta.Pedido = pedido;
            if (!ModelState.IsValid) return View(venta);

            var (existeStock, mensajeError) = await ExisteStock(pedido);
            if (!existeStock)
            {
                CrearAlerta("error", mensajeError!);
                return View(venta);
            }

            return pago == "normal" ? await ProcesarPagoNormal(venta, pedido) : Redirect(await CrearPago(pedido, venta));
        }
        
        [HttpGet]
        private async Task<IActionResult> ProcesarPagoNormal(Venta venta, Pedido pedido)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await ActualizarStock(pedido);
                await RegistrarVenta(venta);
                Console.WriteLine("HOLAAAAAAAAAAAAAA");
                await AsignarVentaAlPedido(venta);
                await transaction.CommitAsync();

                CrearAlerta("success", "Pago exitoso y venta registrada correctamente.");
            }
            catch
            {
                CrearAlerta("error", "Error al registrar la venta.");
                await transaction.RollbackAsync();
            }
            return RedirectToAction("Index", "Producto");
        }

        [HttpGet]
        public async Task<IActionResult> PagoExitoso(string collection_id, string payment_id, string status, string payment_type, int pedidoId, string nombre, string apellido, string telefono, string tipocomprobante, string correo, string direccion, string dni)
        {
            if (status != "approved")
            {
                CrearAlerta("error", "El pago no fue aprobado.");
                return RedirectToAction("Index", "Producto");
            }

            var pedido = await ObtenerPedidoAsync(pedidoId);
            if (pedido == null) return RedirectToAction("Index", "Producto");

            var (existeStock, mensajeError) = await ExisteStock(pedido);
            if (!existeStock) return RedirectToAction("Index", "Producto");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var venta = new Venta
                {
                    Fecha = DateTime.Now,
                    PedidoId = pedido.Id,
                    Pedido = pedido,
                    MetodoPago = "Tarjeta",
                    Nombre = nombre,
                    Apellido = apellido,
                    Telefono = telefono,
                    TipoComprobante = tipocomprobante,
                    Correo = correo,
                    Direccion = direccion,
                    DNI = dni
                };

                await ActualizarStock(pedido);
                await RegistrarVenta(venta);
                await AsignarVentaAlPedido(venta);
                await transaction.CommitAsync();

                CrearAlerta("success", "Pago exitoso y venta registrada correctamente.");
            }
            catch
            {
                CrearAlerta("error", "Error al registrar la venta.");
                await transaction.RollbackAsync();
            }
            ViewBag.CollectionId = collection_id;
            ViewBag.PaymentId = payment_id;
            ViewBag.Status = status;
            ViewBag.PaymentType = payment_type;
            return View();
        }

        [HttpGet]
        public IActionResult PagoFallido()
        {
            CrearAlerta("error", "El pago no fue aprobado.");
            return RedirectToAction("Index", "Producto");
        }

        private async Task<string> CrearPago(Pedido pedido, Venta venta)
        {
            var preferenceRequest = new PreferenceRequest
            {
                Items = pedido.DetallePedidos.Select(d => new PreferenceItemRequest
                {
                    Title = d.Inventario.Producto.Nombre,
                    Quantity = d.Cantidad,
                    CurrencyId = "PEN",
                    UnitPrice = Convert.ToDecimal(d.Inventario.Producto.Precio)
                }).ToList(),
                Payer = new PreferencePayerRequest { Email = "TESTUSER837991704@testuser.com" },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = $"https://localhost:7067/Venta/PagoExitoso?pedidoId={pedido.Id}&nombre={venta.Nombre}&apellido={venta.Apellido}&telefono={venta.Telefono}&tipocomprobante={venta.TipoComprobante}&correo={venta.Correo}&direccion={venta.Direccion}&dni={venta.DNI}",
                    Failure = "https://localhost:7067/Venta/PagoFallido",
                    Pending = "https://localhost:7067/Venta/PagoPendiente"
                },
                AutoReturn = "approved"
            };

            var client = new PreferenceClient();
            var preference = await client.CreateAsync(preferenceRequest);
            return preference.InitPoint;
        }

        private async Task<Pedido?> ObtenerPedidoAsync(int? pedidoId = null, string? userId = null)
        {
            var consulta = _context.Pedidos
                .Include(p => p.DetallePedidos)
                    .ThenInclude(d => d.Inventario)
                    .ThenInclude(i => i.Producto)
                    .ThenInclude(p => p.ImagenProductos)
                .Include(p => p.DetallePedidos)
                    .ThenInclude(d => d.Inventario)
                    .ThenInclude(i => i.Talla)
                .Include(p => p.DetallePedidos)
                    .ThenInclude(d => d.Inventario)
                    .ThenInclude(i => i.Color)
                .Include(p => p.Usuario)
                .AsQueryable();

            return pedidoId.HasValue 
                ? await consulta.FirstOrDefaultAsync(p => p.Id == pedidoId) 
                : await consulta.FirstOrDefaultAsync(p => p.UsuarioId == userId && p.Estado == "Pendiente");
        }

        private async Task<(bool, string?)> ExisteStock(Pedido pedido)
        {
            foreach (var detalle in pedido.DetallePedidos)
            {
                if (detalle.Cantidad > detalle.Inventario.Stock)
                {
                    return (false, $"No hay suficiente stock para {detalle.Inventario.Producto.Nombre}. Disponible: {detalle.Inventario.Stock}, solicitado: {detalle.Cantidad}.");
                }
            }
            return (true, null);
        }

        private async Task ActualizarStock(Pedido pedido)
        {
            foreach (var detalle in pedido.DetallePedidos) detalle.Inventario.Stock -= detalle.Cantidad;
            await _context.SaveChangesAsync();
        }

        private async Task RegistrarVenta(Venta venta)
        {
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();
        }

        private async Task AsignarVentaAlPedido(Venta venta)
        {
            venta.Pedido.VentaId = venta.Id;
            venta.Pedido.Estado = "Pagado";
            await _context.SaveChangesAsync();
        }

        private void CrearAlerta(string tipo, string mensaje)
        {
            TempData["AlertType"] = tipo;
            TempData["AlertMessage"] = mensaje;
        }
    }
}