using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SistemaContext context;

        public HomeController(ILogger<HomeController> logger, SistemaContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            foreach (var item in context.Categorias)
            {
                Console.WriteLine(item.Nombre);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
