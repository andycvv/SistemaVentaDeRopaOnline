using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class TallaController : Controller
    {
        private readonly SistemaContext _sistemaContext;
        public TallaController(SistemaContext sistemaContext)
        {
            _sistemaContext = sistemaContext;
        }

        public async Task<IActionResult> Listar()
        {
            var talla = await _sistemaContext.Tallas.ToListAsync();
            return View(talla);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear([Bind("Id, Nombre")] Talla talla) 
        {
            if (ModelState.IsValid) 
            {
                var duplicado = await _sistemaContext.Tallas.FirstOrDefaultAsync(t => t.Nombre == talla.Nombre);
                if (duplicado == null)
                {
                    _sistemaContext.Tallas.Add(talla);
                    await _sistemaContext.SaveChangesAsync();
                    CrearAlerta("success", "Se registró la talla correctamente");
                }
                else 
                {
                    CrearAlerta("error", "La talla ya existe");
                }

                return RedirectToAction("Listar");
            }

            return View(talla);
        }

        public async Task<IActionResult> Editar(int id) 
        {
            var talla = await _sistemaContext.Tallas.FirstOrDefaultAsync(x => x.Id == id);
            return View(talla);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, [Bind("Id, Nombre, Estado")] Talla talla)
        {
            if (ModelState.IsValid) 
            {
                var duplicado = await _sistemaContext.Tallas.FirstOrDefaultAsync(t => t.Nombre == talla.Nombre);
                if (duplicado == null)
                {
                    _sistemaContext.Tallas.Update(talla);
                    await _sistemaContext.SaveChangesAsync();
                    CrearAlerta("success", "Se editó la talla correctamente");
                }
                else 
                {
                    CrearAlerta("error", "La talla ya existe");
                }
                
                return RedirectToAction("Listar");
            }

            return View(talla);
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id) 
        {
            var talla = await _sistemaContext.Tallas.FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                _sistemaContext.Tallas.Remove(talla);
                await _sistemaContext.SaveChangesAsync();
                CrearAlerta("success", "Se elimino la talla correctamente");
            }
            catch 
            {
                CrearAlerta("error", "No se puede eliminar la talla, porque está siendo utilizada en el inventario");
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
