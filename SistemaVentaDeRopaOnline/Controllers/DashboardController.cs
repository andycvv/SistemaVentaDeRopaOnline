using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models.ViewModels;

namespace SistemaVentaDeRopaOnline.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class DashboardController : Controller
    {
        private readonly SistemaContext _context;

        public DashboardController(SistemaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var dashboardData = new DashboardViewModel()
            {
                TotalUsuarios = await _context.Usuarios.CountAsync(),
                TotalVentas = await _context.Ventas.CountAsync(),
                TotalProductos = await _context.Productos.CountAsync(),
                TotalPedidos = await _context.Pedidos.CountAsync(),
                GananciasPendientes = await _context.Pedidos.Where(p => p.Estado == "Pendiente").SumAsync(v => v.Total),
                GananciasConfirmadas = await _context.Pedidos.Where(p => p.Estado == "Pagado").SumAsync(v => v.Total),
                UltimosProductosCreados = await _context.Productos
                    .OrderByDescending(p => p.Id)
                    .Take(6)
                    .Include(p => p.ImagenProductos)
                    .Include(p => p.Inventarios)
                    .Where(p => p.Inventarios.Count > 0)
                    .ToListAsync()
            };

            return View(dashboardData);
        }
    }
}
