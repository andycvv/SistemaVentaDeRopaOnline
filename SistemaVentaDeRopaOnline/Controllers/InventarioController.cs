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

        public void CrearAlerta(string alertType, string alertMessage)
        {
            TempData["AlertMessage"] = alertMessage;
            TempData["AlertType"] = alertType;
        }
    }
}
