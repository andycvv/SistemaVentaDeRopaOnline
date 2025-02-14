using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public async Task<IActionResult> Index(string? genero)
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
                    .Include(p => p.Categoria)
                    .AsQueryable();

            if (!genero.IsNullOrEmpty())
            {
                query = query.Where(p => p.Genero == genero);
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

        public async Task<IActionResult> Listar()
        {
            var productos = await GetAllProducts(null);
            return View(productos);
        }

        public async Task<IActionResult> Crear()
        {
            var categorias = await context.Categorias.Where(c => c.Estado).ToListAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear([Bind("Id, Nombre, Precio, Genero, Descripcion, Marca, Estado, CategoriaId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                var duplicado = await context.Productos.FirstOrDefaultAsync(p => p.Nombre == producto.Nombre);
                if (duplicado == null)
                {
                    context.Productos.Add(producto);
                    await context.SaveChangesAsync();
                    CrearAlerta("success", "Se registró el producto correctamente");
                }
                else
                {
                    CrearAlerta("error", "El producto ya existe");
                }

                return RedirectToAction("Listar");
            }
            var categorias = await context.Categorias.Where(c => c.Estado).ToListAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");

            return View(producto);
        }

        public async Task<IActionResult> Estado(int id)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(p => p.Id == id);
            producto.Estado = !producto.Estado;
            await context.SaveChangesAsync();
            CrearAlerta("success", "Se actualizó el estado correctamente");

            return RedirectToAction("Listar");
        }

        public async Task<IActionResult> Editar(int id)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(p => p.Id == id);
            var categorias = await context.Categorias.Where(c => c.Estado).ToListAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");
            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, [Bind("Id, Nombre, Precio, Genero, Descripcion, Marca, CategoriaId, Estado")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                var duplicado = await context.Productos.FirstOrDefaultAsync(p => p.Nombre == producto.Nombre && p.Id != producto.Id);
                if (duplicado == null)
                {
                    context.Productos.Update(producto);
                    await context.SaveChangesAsync();
                    CrearAlerta("success", "Se editó el producto correctamente");
                }
                else
                {
                    CrearAlerta("error", "El producto ya existe");
                }

                return RedirectToAction("Listar");
            }
            var categorias = await context.Categorias.Where(c => c.Estado).ToListAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");

            return View(producto);
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(p => p.Id == id);

            try
            {
                context.Productos.Remove(producto);
                await context.SaveChangesAsync();
                CrearAlerta("success", "Se elimino el producto correctamente");
            }
            catch 
            {
                CrearAlerta("error", "No se puede eliminar el producto, porque está siendo utilizado en el inventario");
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
