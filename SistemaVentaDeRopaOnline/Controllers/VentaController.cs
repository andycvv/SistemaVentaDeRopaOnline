using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class VentaController : Controller
    {
        private readonly SistemaContext _context;

        public VentaController(SistemaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Listar()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Pedido)
                .ToListAsync();
            return View(ventas);
        }

        public IActionResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se registró la venta correctamente");
                return RedirectToAction("Listar");
            }
            return View(venta);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            return View(venta);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Update(venta);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se editó la venta correctamente");
                return RedirectToAction("Listar");
            }
            return View(venta);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se eliminó la venta correctamente");
            }
            else
            {
                CrearAlerta("error", "La venta no existe");
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