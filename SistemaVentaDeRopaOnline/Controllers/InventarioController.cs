using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class InventarioController : Controller
    {
        private readonly SistemaContext _sistemaContext;
        public InventarioController(SistemaContext sistemaContext)
        { 
            _sistemaContext = sistemaContext;
        }

        public async Task<IActionResult> Listar()
        {
            var inventarios = await _sistemaContext.Inventarios.Include(i => i.Producto).Include(i => i.Talla).Include(i => i.Color).ToListAsync();
            return View(inventarios);
        }
    }
}
