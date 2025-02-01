using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class ProductoController : Controller
    {
        private readonly SistemaContext context;
        public ProductoController(SistemaContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Listado(string? genero)
        {
            var productos = await GetAllProducts(genero);
            ViewBag.genero = genero;
            return View(productos);
        }
        private async Task<List<Producto>> GetAllProducts(string? genero)
        {
            var query = context.Productos
                    .Include(p => p.ImagenProductos)
                    .AsQueryable();

            if (!genero.IsNullOrEmpty())
            {
                query = query.Where(p => p.Genero == genero)
                             .Include(p => p.Categoria);
            }

            return await query.ToListAsync();
        }
    }
}
