using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class PedidoController : Controller
    {
        private readonly SistemaContext _context;

        public PedidoController(SistemaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Listar()
        {
            var pedidos = await _context.Pedidos.ToListAsync();
            return View(pedidos);
        }

        public IActionResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se registró el pedido correctamente");
                return RedirectToAction("Listar");
            }
            return View(pedido);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            return View(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Update(pedido);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se editó el pedido correctamente");
                return RedirectToAction("Listar");
            }
            return View(pedido);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se eliminó el pedido correctamente");
            }
            else
            {
                CrearAlerta("error", "El pedido no existe");
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