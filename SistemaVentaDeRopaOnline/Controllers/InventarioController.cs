using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class InventarioController : Controller
    {
        private readonly SistemaContext _context;

        public InventarioController(SistemaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Listar()
        {
            var inventarios = await _context.Inventarios
                .Include(i => i.Producto)
                .Include(i => i.Color)
                .Include(i => i.Talla)
                .ToListAsync();
            return View(inventarios);
        }

        public IActionResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                _context.Inventarios.Add(inventario);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se registró el inventario correctamente");
                return RedirectToAction("Listar");
            }
            return View(inventario);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var inventario = await _context.Inventarios.FindAsync(id);
            return View(inventario);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                _context.Update(inventario);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se editó el inventario correctamente");
                return RedirectToAction("Listar");
            }
            return View(inventario);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario != null)
            {
                _context.Inventarios.Remove(inventario);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se eliminó el inventario correctamente");
            }
            else
            {
                CrearAlerta("error", "El inventario no existe");
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