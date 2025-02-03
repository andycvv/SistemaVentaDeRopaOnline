using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;

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
    }
}
