using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;
using System.Runtime.CompilerServices;

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
        }
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
                    if (!await ExisteStock(pedido))
                    {
                        CrearAlerta("error", "No hay suficiente stock");
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
        private async Task<bool> ExisteStock(Pedido pedido)
        {
            foreach (var detalle in pedido.DetallePedidos)
            {
                if (detalle.Cantidad > detalle.Inventario.Stock)
                {
                    return false;
                }
            }
            return true;
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
