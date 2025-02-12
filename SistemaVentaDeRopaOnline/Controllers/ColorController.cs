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
        public async Task<IActionResult> Crear([Bind("Id, Nombre, Codigo")] Color color)
        {
            if (ModelState.IsValid)
            {
                var duplicado = await _context.Colores.FirstOrDefaultAsync(c => c.Nombre == color.Nombre);
                if (duplicado == null)
                {
                    _context.Colores.Add(color);
                    await _context.SaveChangesAsync();
                    CrearAlerta("success", "Se registró el color correctamente");
                }
                else
                {
                    CrearAlerta("error", "El color ya existe");
                }

                return RedirectToAction("Listar");
            }

            return View(color);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var color = await _context.Colores.FirstOrDefaultAsync(x => x.Id == id);
            return View(color);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, [Bind("Id, Nombre, Codigo")] Color color)
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
                }
                else
                {
                    CrearAlerta("error", "El color ya existe");
                }

                return RedirectToAction("Listar");
            }

            return View(color);
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var color = await _context.Colores.FirstOrDefaultAsync(x => x.Id == id);
            if (color != null)
            {
                _context.Colores.Remove(color);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se eliminó el color correctamente");
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