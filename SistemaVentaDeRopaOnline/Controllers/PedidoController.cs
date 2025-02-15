using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
            {
                CrearAlerta("error", "Debe ingresar con su cuenta para realizar un pedido.");
                return RedirectToAction("Index", "Producto");
            }

            var pedido = await context.Pedidos
                .Include(p => p.DetallePedidos)
                    .ThenInclude(d => d.Inventario)
                    .ThenInclude(i => i.Producto)
                    .ThenInclude(p => p.ImagenProductos)
                .Include(p => p.DetallePedidos)
                    .ThenInclude(d => d.Inventario)
                    .ThenInclude(i => i.Color)
                .Include(p => p.DetallePedidos)
                    .ThenInclude(d => d.Inventario)
                    .ThenInclude(i => i.Talla)
                .Where(p => p.UsuarioId == u.Id)
                .FirstOrDefaultAsync();

            if (pedido == null) pedido = new Pedido();

            return View(pedido);
        }

        public void CrearAlerta(string alertType, string alertMessage)
        {
            TempData["AlertMessage"] = alertMessage;
            TempData["AlertType"] = alertType;
        }
    }
}
