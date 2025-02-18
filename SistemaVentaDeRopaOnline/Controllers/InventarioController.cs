using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;

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

        public async Task<IActionResult> Crear()
        {
            var productos = await _sistemaContext.Productos.Where(p => p.Estado).ToListAsync();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            var tallas = await _sistemaContext.Tallas.Where(t => t.Estado).ToListAsync();
            ViewBag.Tallas = new SelectList(tallas, "Id", "Nombre");

            var colores = await _sistemaContext.Colores.Where(c => c.Estado).ToListAsync();
            ViewBag.Colores = new SelectList(colores, "Id", "Nombre");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear([Bind("Id, ProductoId, TallaId, ColorId, Stock")] Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                var duplicado = await _sistemaContext.Inventarios.FirstOrDefaultAsync(i => i.ProductoId == inventario.ProductoId && i.TallaId == inventario.TallaId && i.ColorId == inventario.ColorId);
                if (duplicado == null)
                {
                    _sistemaContext.Inventarios.Add(inventario);
                    await _sistemaContext.SaveChangesAsync();
                    CrearAlerta("success", "Se registró el inventario correctamente");
                }
                else
                {
                    CrearAlerta("error", "El inventario ya existe");
                }

                return RedirectToAction("Listar");
            }

            var productos = await _sistemaContext.Productos.Where(p => p.Estado).ToListAsync();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            var tallas = await _sistemaContext.Tallas.Where(t => t.Estado).ToListAsync();
            ViewBag.Tallas = new SelectList(tallas, "Id", "Nombre");

            var colores = await _sistemaContext.Colores.Where(c => c.Estado).ToListAsync();
            ViewBag.Colores = new SelectList(colores, "Id", "Nombre");

            return View(inventario);
        }

        public async Task<IActionResult> Estado(int id)
        {
            var inventario = await _sistemaContext.Inventarios.FirstOrDefaultAsync(i => i.Id == id);
            inventario.Estado = !inventario.Estado;
            await _sistemaContext.SaveChangesAsync();
            CrearAlerta("success", "Se actualizó el estado correctamente");

            return RedirectToAction("Listar");
        }

        public async Task<IActionResult> Editar(int id)
        {
            var inventario = await _sistemaContext.Inventarios.FirstOrDefaultAsync(i => i.Id == id);

            var productos = await _sistemaContext.Productos.Where(p => p.Estado).ToListAsync();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            var tallas = await _sistemaContext.Tallas.Where(t => t.Estado).ToListAsync();
            ViewBag.Tallas = new SelectList(tallas, "Id", "Nombre");

            var colores = await _sistemaContext.Colores.Where(c => c.Estado).ToListAsync();
            ViewBag.Colores = new SelectList(colores, "Id", "Nombre");

            return View(inventario);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, [Bind("Id, ProductoId, TallaId, ColorId, Stock, Estado")] Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                var duplicado = await _sistemaContext.Inventarios.FirstOrDefaultAsync(i => i.ProductoId == inventario.ProductoId && i.TallaId == inventario.TallaId && i.ColorId == inventario.ColorId && i.Id != inventario.Id);
                if (duplicado == null)
                {
                    _sistemaContext.Inventarios.Update(inventario);
                    await _sistemaContext.SaveChangesAsync();
                    CrearAlerta("success", "Se editó el inventario correctamente");
                }
                else
                {
                    CrearAlerta("error", "El inventario ya existe");
                }

                return RedirectToAction("Listar");
            }

            var productos = await _sistemaContext.Productos.Where(p => p.Estado).ToListAsync();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            var tallas = await _sistemaContext.Tallas.Where(t => t.Estado).ToListAsync();
            ViewBag.Tallas = new SelectList(tallas, "Id", "Nombre");

            var colores = await _sistemaContext.Colores.Where(c => c.Estado).ToListAsync();
            ViewBag.Colores = new SelectList(colores, "Id", "Nombre");

            return View(inventario);
        }

        public async Task<IActionResult> Eliminar(int id) 
        {
            var inventario = await _sistemaContext.Inventarios.FirstOrDefaultAsync(i => i.Id == id);

            try
            {
                _sistemaContext.Inventarios.Remove(inventario);
                await _sistemaContext.SaveChangesAsync();
                CrearAlerta("success", "Se elimino el inventario correctamente");
            }
            catch
            {
                CrearAlerta("error", "No se puede eliminar el inventario, porque está siendo utilizado");
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
