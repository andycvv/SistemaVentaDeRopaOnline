using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models.ViewModels;
using SistemaVentaDeRopaOnline.Models;
using Microsoft.AspNetCore.Authorization;

namespace SistemaVentaDeRopaOnline.Controllers
{
    [Authorize(Roles = "Administrador")]
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

        public async Task<IActionResult> Eliminar(int id, int productoId)
        {
            var imagen = await _sistemaContext.Imagenes.FirstOrDefaultAsync(i => i.Id == id);

            try
            {
                _sistemaContext.Imagenes.Remove(imagen);
                await _sistemaContext.SaveChangesAsync();
                CrearAlerta("success", "Se elimino la imagen correctamente");
            }
            catch
            {
                CrearAlerta("error", "No se puede eliminar la imagen, porque está siendo utilizado en el inventario");
            }
 
            return RedirectToAction("Asignar", new { id = productoId });
        }

        public void CrearAlerta(string alertType, string alertMessage)
        {
            TempData["AlertMessage"] = alertMessage;
            TempData["AlertType"] = alertType;
        }
    }
}
