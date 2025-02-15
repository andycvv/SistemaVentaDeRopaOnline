using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class DetallePedidoController : Controller
    {
        private readonly SistemaContext _context;

        public DetallePedidoController(SistemaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Listar()
        {
            var detalles = await _context.DetallePedidos
                .Include(d => d.Pedido)
                .Include(d => d.Inventario)
                .ThenInclude(i => i.Producto)
                .ToListAsync();
            return View(detalles);
        }

        public IActionResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(DetallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                _context.DetallePedidos.Add(detallePedido);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se registró el detalle de pedido correctamente");
                return RedirectToAction("Listar");
            }
            return View(detallePedido);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var detallePedido = await _context.DetallePedidos.FindAsync(id);
            return View(detallePedido);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, DetallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                _context.Update(detallePedido);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se editó el detalle de pedido correctamente");
                return RedirectToAction("Listar");
            }
            return View(detallePedido);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var detallePedido = await _context.DetallePedidos.FindAsync(id);
            if (detallePedido != null)
            {
                _context.DetallePedidos.Remove(detallePedido);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se eliminó el detalle de pedido correctamente");
            }
            else
            {
                CrearAlerta("error", "El detalle de pedido no existe");
            }

            return RedirectToAction("Listar");
        }

        public void CrearAlerta(string alertType, string alertMessage)
        {
            TempData["AlertMessage"] = alertMessage;
            TempData["AlertType"] = alertType;
        }
    }
}