using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
