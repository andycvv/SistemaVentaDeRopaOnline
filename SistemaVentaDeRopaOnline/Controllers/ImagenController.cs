using Microsoft.AspNetCore.Mvc;
using SistemaVentaDeRopaOnline.Data;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class ImagenController : Controller
    {
        private readonly SistemaContext _sistemaContext;
        public ImagenController(SistemaContext sistemaContext)
        {
            _sistemaContext = sistemaContext;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
