using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly SistemaContext _sistemaContext;

        public CategoriaController(SistemaContext sistemaContext)
        {
            _sistemaContext = sistemaContext;
        }

        public async Task<IActionResult> Listar()
        {
            var categorias = await _sistemaContext.Categorias.ToListAsync();
            return View(categorias);
        }

        public IActionResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear([Bind("Id, Nombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var duplicado = await _sistemaContext.Categorias.FirstOrDefaultAsync(c => c.Nombre == categoria.Nombre);
                if (duplicado == null)
                {
                    _sistemaContext.Categorias.Add(categoria);
                    await _sistemaContext.SaveChangesAsync();
                    CrearAlerta("success", "Se registró la categoría correctamente");
                }
                else
                {
                    CrearAlerta("error", "La categoría ya existe");
                }

                return RedirectToAction("Listar");
            }

            return View(categoria);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var categoria = await _sistemaContext.Categorias.FirstOrDefaultAsync(c => c.Id == id);
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, [Bind("Id, Nombre, Estado")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var duplicado = await _sistemaContext.Categorias
                    .FirstOrDefaultAsync(c => c.Nombre == categoria.Nombre && c.Id != categoria.Id);
                if (duplicado == null)
                {
                    _sistemaContext.Categorias.Update(categoria);
                    await _sistemaContext.SaveChangesAsync();
                    CrearAlerta("success", "Se editó la categoría correctamente");
                }
                else
                {
                    CrearAlerta("error", "La categoría ya existe");
                }

                return RedirectToAction("Listar");
            }

            return View(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> Estado(int id)
        {
            var categoria = await _sistemaContext.Categorias.FirstOrDefaultAsync(c => c.Id == id);
            if (categoria != null)
            {
                categoria.Estado = !categoria.Estado;
                await _sistemaContext.SaveChangesAsync();
                CrearAlerta("success", "Se actualizó el estado correctamente");
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var categoria = await _sistemaContext.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            try
            {
                _sistemaContext.Categorias.Remove(categoria);
                await _sistemaContext.SaveChangesAsync();
                CrearAlerta("success", "Se eliminó la categoría correctamente");
            }
            catch
            {
                CrearAlerta("error", "No se puede eliminar la categoría, porque está siendo utilizada en el inventario");
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