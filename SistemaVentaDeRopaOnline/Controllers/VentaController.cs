using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;
using System.Runtime.CompilerServices;
using MercadoPago.Config;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class VentaController : Controller
    {
        private readonly SistemaContext context;
        private readonly UserManager<Usuario> _userManager;
        public VentaController(SistemaContext context,UserManager<Usuario> userManager)
        {
            this.context = context;
            _userManager = userManager;
            MercadoPagoConfig.AccessToken = "APP_USR-3117456896561429-021809-e122fff0dc26478fbe16ef17906d9342-2275386132";
        }
        [HttpGet]
        public async Task<IActionResult> Crear ()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Producto");
            }

            var pedido = await ObtenerPedidoAsync(userId: user.Id);

            if (pedido == null)
            {
                return RedirectToAction("Index", "Producto");
            }

            Venta venta = new Venta()
            {
                Fecha = DateTime.Now,
                PedidoId = pedido.Id,
                Pedido = pedido,
                MetodoPago = "Tarjeta"
            };

            string urlPago = await CrearPago(venta);
            ViewBag.UrlPago = urlPago;

            return View(venta);
        }
        [HttpPost]
        public async Task<IActionResult> Crear(Venta venta)
        {
            var pedido = await ObtenerPedidoAsync(pedidoId: venta.PedidoId);
            if (pedido == null)
            {
                return RedirectToAction("Index", "Producto");
            }
            venta.Pedido = pedido;

            if (!ModelState.IsValid)
            {
                return View(venta);
            }

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var (existeStock, mensajeError) = await ExisteStock(pedido);
                    if (!existeStock)
                    {
                        CrearAlerta("error", mensajeError!);
                        return View(venta);
                    }

                    await ActualizarStock(pedido);
                    await RegistrarVenta(venta);
                    await AsignarVentaAlPedido(venta);

                    await transaction.CommitAsync();

                    CrearAlerta("success", "Se registro la venta correctamente");
                    return RedirectToAction("Index", "Pedido");
                } 
                catch
                {
                    CrearAlerta("error", "Algo salio mal al registrar la venta");
                    return View(venta);
                }
            }
        }
        [HttpGet]
        public IActionResult PagoExitoso(string collection_id, string payment_id, string status, string payment_type)
        {
            ViewBag.CollectionId = collection_id;
            ViewBag.PaymentId = payment_id;
            ViewBag.Status = status;
            ViewBag.PaymentType = payment_type;

            return View();
        }
        private async Task<string> CrearPago(Venta venta)
        {
            var preferenceRequest = new PreferenceRequest
            {
                Items = venta.Pedido.DetallePedidos.Select(d => new PreferenceItemRequest
                {
                    Title = d.Inventario.Producto.Nombre,
                    Quantity = d.Cantidad,
                    CurrencyId = "PEN",
                    UnitPrice = Convert.ToDecimal(d.Inventario.Producto.Precio)
                }).ToList(),
                Payer = new PreferencePayerRequest
                {
                    Email = "TESTUSER837991704@testuser.com"
                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "https://localhost:7067/Venta/PagoExitoso",
                    Failure = "https://localhost:7067/Venta/PagoFallido",
                    Pending = "https://localhost:7067//Venta/PagoPendiente"
                },
                AutoReturn = "approved"
            };

            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(preferenceRequest);

            return preference.InitPoint;
        }
        private async Task<Pedido?> ObtenerPedidoAsync(int? pedidoId = null, string? userId = null)
        {
            var consulta = context.Pedidos
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

            if (pedidoId != null)
            {
                return await consulta.FirstOrDefaultAsync(p => p.Id == pedidoId);
            }

            if (userId != null)
            {
                return await consulta.FirstOrDefaultAsync(p => p.UsuarioId == userId && p.Estado == "Pendiente");
            }

            return null;
        }
        private async Task<(bool existeStock, string? mensajeError)> ExisteStock(Pedido pedido)
        {
            foreach (var detalle in pedido.DetallePedidos)
            {
                if (detalle.Cantidad > detalle.Inventario.Stock)
                {
                    string mensajeError = $"No hay suficiente stock para el producto {detalle.Inventario.Producto.Nombre}. " +
                      $"Stock disponible: {detalle.Inventario.Stock}, solicitado: {detalle.Cantidad}.";
                    return (false, mensajeError);
                }
            }
            return (true, null);
        }
        private async Task ActualizarStock (Pedido pedido)
        {
            foreach (var detalle in pedido.DetallePedidos)
            {
                detalle.Inventario.Stock -= detalle.Cantidad;
            }
            await context.SaveChangesAsync();
        }
        private async Task RegistrarVenta (Venta venta)
        {
            context.Ventas.Add(venta);
            await context.SaveChangesAsync();
        }
        private async Task AsignarVentaAlPedido(Venta venta)
        {
            venta.Pedido.VentaId = venta.Id;
            venta.Pedido.Estado = "Pagado";
            await context.SaveChangesAsync();
        }
        public void CrearAlerta(string alertType, string alertMessage)
        {
            TempData["AlertMessage"] = alertMessage;
            TempData["AlertType"] = alertType;
        }
    }
}
