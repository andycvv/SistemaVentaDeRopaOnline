using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class PedidoController : Controller
    {
        private readonly SistemaContext context;
        private readonly UserManager<Usuario> _userManager;
        public PedidoController(UserManager<Usuario> userManager, SistemaContext context)
        {
            this.context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
