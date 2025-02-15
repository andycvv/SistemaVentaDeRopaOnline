﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class PedidoController : Controller
    {
        private readonly SistemaContext context;
        private readonly UserManager<Usuario> _userManager;
        public PedidoController(UserManager<Usuario> userManager, SistemaContext context)
        {
            this.context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
            {
                CrearAlerta("error", "Debe ingresar con su cuenta para realizar un pedido.");
                return RedirectToAction("Index", "Producto");
            }

            var pedido = await context.Pedidos
                .Include(p => p.DetallePedidos)
                    .ThenInclude(d => d.Inventario)
                    .ThenInclude(i => i.Producto)
                    .ThenInclude(p => p.ImagenProductos)
                .Include(p => p.DetallePedidos)
                    .ThenInclude(d => d.Inventario)
                    .ThenInclude(i => i.Color)
                .Include(p => p.DetallePedidos)
                    .ThenInclude(d => d.Inventario)
                    .ThenInclude(i => i.Talla)
                .Where(p => p.UsuarioId == usuario.Id)
                .FirstOrDefaultAsync();

            if (pedido == null) pedido = new Pedido();

            return View(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(int productoId, double precio, int ideTal, int ideCol)
        {
            if (ideTal == 0 || ideCol == 0)
            {
                TempData["Advertencia"] = "Elija una talla y un color.";
                return RedirectToAction("Detalle", "Producto", new { id = productoId });
            }

            var usuario = await _userManager.GetUserAsync(User);

            if(usuario == null)
            {
                CrearAlerta("error", "Debe ingresar con su cuenta para realizar un pedido.");
                return RedirectToAction("Detalle", "Producto", new { id = productoId });
            }

            string usuarioId = usuario.Id;

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var pedidoExistente = await ObtenerPedidoPendiente(usuarioId);
                    if (pedidoExistente == null)
                    {
                        pedidoExistente = await CrearNuevoPedido(usuarioId, precio);
                    }

                    var inventario = await ObtenerInventario(productoId, ideTal, ideCol);

                    if (inventario == null)
                    {
                        CrearAlerta("error", "Producto no disponible.");
                        return RedirectToAction("Index");
                    }
                    
                    var detalleExistente = await ObtenerDetallePedidoExistente(pedidoExistente.Id, inventario);

                    if (detalleExistente == null)
                    {
                        detalleExistente = await CrearDetallePedido(pedidoExistente, precio, inventario);
                    }
                    else
                    {
                        await ActualizarDetallePedido(detalleExistente, precio);
                    }

                    await ActualizarTotalPedido(pedidoExistente);

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    CrearAlerta("error", "Algo salio mal al crear el pedido...");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        private async Task<Pedido?> ObtenerPedidoPendiente(string usuarioId)
        {
            return await context.Pedidos
                .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.Estado == "Pendiente");
        }

        private async Task<Pedido> CrearNuevoPedido(string usuarioId, double precio)
        {
            var nuevoPedido = new Pedido
            {
                Fecha = DateTime.Now,
                Estado = "Pendiente",
                Total = precio,
                UsuarioId = usuarioId
            };

            context.Pedidos.Add(nuevoPedido);
            await context.SaveChangesAsync();
            return nuevoPedido;
        }

        private async Task<DetallePedido?> ObtenerDetallePedidoExistente(int pedidoId, Inventario inventario)
        {
            return await context.DetallePedidos
                .FirstOrDefaultAsync(
                    dp => dp.PedidoId == pedidoId && 
                    dp.Inventario == inventario
                );
        }

        private async Task<Inventario?> ObtenerInventario(int productoId, int ideTal, int ideCol)
        {
            return await context.Inventarios
                .FirstOrDefaultAsync(
                    i => i.ProductoId == productoId && i.TallaId == ideTal && i.ColorId == ideCol
                );
        }

        private async Task<DetallePedido> CrearDetallePedido(Pedido pedidoExistente, double precio, Inventario inventario)
        {
            var detallePedido = new DetallePedido
            {
                PedidoId = pedidoExistente.Id,
                Inventario = inventario,
                Cantidad = 1,
                Precio = precio
            };

            context.DetallePedidos.Add(detallePedido);
            await context.SaveChangesAsync();
            return detallePedido;
        }
        
        private async Task ActualizarTotalPedido(Pedido pedido)
        {
            pedido.Total = await context.DetallePedidos
                .Where(dp => dp.PedidoId == pedido.Id)
                .SumAsync(dp => dp.Precio);

            context.Pedidos.Update(pedido);
            await context.SaveChangesAsync();
        }

        private async Task ActualizarDetallePedido(DetallePedido detallePedido, double precio)
        {
            detallePedido.Cantidad += 1;
            detallePedido.Precio = detallePedido.Cantidad * precio;

            context.DetallePedidos.Update(detallePedido);
            await context.SaveChangesAsync();
        }

        private async Task ActualizarInventario(Inventario inventario)
        {
            if (inventario != null && inventario.Stock > 0)
            {
                inventario.Stock -= 1;
                context.Inventarios.Update(inventario);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Stock no disponible o insuficiente.");
            }
        }

        public void CrearAlerta(string alertType, string alertMessage)
        {
            TempData["AlertMessage"] = alertMessage;
            TempData["AlertType"] = alertType;
        }
    }
}
