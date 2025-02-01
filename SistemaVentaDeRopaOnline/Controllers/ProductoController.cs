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
        public async Task<IActionResult> Detalle(int id, int? ideTal)
        {
            var producto = await GetProduct(id, ideTal);
            if (ideTal != null)
            {
                ViewBag.IdeTal = ideTal;
            }
            return View(producto);
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
        private async Task<Producto?> GetProduct(int id, int? ideTal)
        {
            var query = context.Productos
                .Include(p => p.ImagenProductos)
                .Include(p => p.Inventarios)
                    .ThenInclude(i => i.Talla)
                .Include(p => p.Inventarios)
                    .ThenInclude(i => i.Color)
                .Where(p => p.Id == id)
                .AsQueryable();

            if (ideTal.HasValue)
            {
                query = query
                    .Where(p => p.Inventarios.Any(i => i.TallaId == ideTal.Value));
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
