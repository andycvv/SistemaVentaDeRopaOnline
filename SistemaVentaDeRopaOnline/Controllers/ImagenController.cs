using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class ImagenController : Controller
    {
        private readonly SistemaContext _context;

        public ImagenController(SistemaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Listar()
        {
            var imagenes = await _context.Imagenes.ToListAsync();
            return View(imagenes);
        }

        public IActionResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(Imagen imagen)
        {
            if (ModelState.IsValid)
            {
                _context.Imagenes.Add(imagen);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se registró la imagen correctamente");
                return RedirectToAction("Listar");
            }
            return View(imagen);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var imagen = await _context.Imagenes.FindAsync(id);
            return View(imagen);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Imagen imagen)
        {
            if (ModelState.IsValid)
            {
                _context.Update(imagen);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se editó la imagen correctamente");
                return RedirectToAction("Listar");
            }
            return View(imagen);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var imagen = await _context.Imagenes.FindAsync(id);
            if (imagen != null)
            {
                _context.Imagenes.Remove(imagen);
                await _context.SaveChangesAsync();
                CrearAlerta("success", "Se eliminó la imagen correctamente");
            }
            else
            {
                CrearAlerta("error", "La imagen no existe");
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