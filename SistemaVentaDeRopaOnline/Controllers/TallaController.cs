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
                _sistemaContext.Tallas.Add(talla);
                await _sistemaContext.SaveChangesAsync();
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
        public async Task<IActionResult> Editar(int id, [Bind("Id, Nombre")] Talla talla)
        {
            if (ModelState.IsValid) 
            {
                _sistemaContext.Tallas.Update(talla);
                await _sistemaContext.SaveChangesAsync();
                return RedirectToAction("Listar");
            }

            return View(talla);
        }
    }
}
