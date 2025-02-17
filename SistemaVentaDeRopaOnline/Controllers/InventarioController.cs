using Microsoft.AspNetCore.Mvc;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class InventarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
