using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models.ViewModels;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class ImagenController : Controller
    {
        private readonly SistemaContext _sistemaContext;
        public ImagenController(SistemaContext sistemaContext)
        {
            _sistemaContext = sistemaContext;
        }

        public async Task<IActionResult> Asignar(int id)
        {
            var producto = await _sistemaContext.Productos.FirstOrDefaultAsync(p => p.Id == id);
            var imagenes = await _sistemaContext.Imagenes.Where(i => i.ProductoId == producto.Id).ToListAsync();
            
            AsignarImagenesViewModel modelo = new AsignarImagenesViewModel()
            {
                Producto = producto,
                Imagenes = imagenes
            };

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Asignar(int ProductoId, string Url)
        {
            var producto = await _sistemaContext.Productos.FirstOrDefaultAsync(p => p.Id == ProductoId);

            Imagen imagen = new Imagen()
            {
                ProductoId = ProductoId,
                Url = Url
            };

            _sistemaContext.Add(imagen);
            await _sistemaContext.SaveChangesAsync();

            var imagenes = await _sistemaContext.Imagenes.Where(i => i.ProductoId == producto.Id).ToListAsync();

            AsignarImagenesViewModel modelo = new AsignarImagenesViewModel()
            {
                Producto = producto,
                Imagenes = imagenes
            };

            return View(modelo);
        }
    }
}
