using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class ColorController : Controller
    {
        private readonly SistemaContext _context;

        public ColorController(SistemaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Listar()
        {
            var colores = await _context.Colores.ToListAsync();
            return View(colores);
        }

        public IActionResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear([Bind("Id, Nombre, Codigo, Estado")] Color color)
        {
            if (ModelState.IsValid)
            {
                var duplicado = await _context.Colores.FirstOrDefaultAsync(c => c.Nombre == color.Nombre);
                if (duplicado == null)
                {
                    _context.Colores.Add(color);
                    await _context.SaveChangesAsync();
                    CrearAlerta("success", "Se registró el color correctamente");
                    return RedirectToAction("Listar");
                }
                else
                {
                    CrearAlerta("error", "El color ya existe");
                }
            }
            return View(color);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var color = await _context.Colores.FirstOrDefaultAsync(c => c.Id == id);
            return View(color);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, [Bind("Id, Nombre, Codigo, Estado")] Color color)
        {
            if (ModelState.IsValid)
            {
                var duplicado = await _context.Colores
                    .FirstOrDefaultAsync(c => c.Nombre == color.Nombre && c.Id != color.Id);
                if (duplicado == null)
                {
                    _context.Colores.Update(color);
                    await _context.SaveChangesAsync();
                    CrearAlerta("success", "Se editó el color correctamente");
                    return RedirectToAction("Listar");
                }
                else
                {
                    CrearAlerta("error", "El color ya existe");
                }
            }
            return View(color);
        }

        [HttpGet]
        public async Task<IActionResult> Estado(int id)
        {
            var color = await _context.Colores.FirstOrDefaultAsync(c => c.Id == id);
            if (color != null)
            {
                color.Estado = !color.Estado; // Cambia el estado
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se actualizó el estado del color correctamente");
            }
            else
            {
                CrearAlerta("error", "El color no existe");
            }
            return RedirectToAction("Listar");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var color = await _context.Colores.FirstOrDefaultAsync(c => c.Id == id);
            if (color != null)
            {
                try
                {
                    _context.Colores.Remove(color);
                    await _context.SaveChangesAsync();
                    CrearAlerta("success", "Se eliminó el color correctamente");
                }
                catch
                {
                    CrearAlerta("error", "No se puede eliminar el color, porque está siendo utilizado en el inventario");
                }
            }
            else
            {
                CrearAlerta("error", "El color no existe");
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