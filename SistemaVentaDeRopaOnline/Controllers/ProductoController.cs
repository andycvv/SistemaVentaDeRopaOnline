using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Index()
        {
            return View();
        }
    }
}
