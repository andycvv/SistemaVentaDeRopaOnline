using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class RolController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Usuario> _userManager;
        public RolController(RoleManager<IdentityRole> roleManager, UserManager<Usuario> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Listar()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
                CrearAlerta("success", "Se registró el rol correctamente");
                return RedirectToAction("Listar");
            }
            CrearAlerta("error", "Este rol ya está registrado");
            return View();
        }

        public async Task<IActionResult> Editar(string id)
        {
            var rol = await _roleManager.FindByIdAsync(id);
            return View(rol);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(string Id, string Name)
        {
            var duplicado = await _roleManager.Roles.Where(r => r.Name == Name && r.Id != Id).FirstOrDefaultAsync();

            if (duplicado == null)
            {
                var rol = await _roleManager.FindByIdAsync(Id);
                rol.Name = Name;
                await _roleManager.UpdateAsync(rol);
                CrearAlerta("success", "Se editó el rol correctamente");
            }
            else
            {
                CrearAlerta("error", "Este rol ya está registrado");
            }
            return RedirectToAction("Listar");
        }

        public async Task<IActionResult> Eliminar(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
            CrearAlerta("success", "Se elimino el rol correctamente");
            return RedirectToAction("Listar");
        }

        public void CrearAlerta(string alertType, string alertMessage)
        {
            TempData["AlertMessage"] = alertMessage;
            TempData["AlertType"] = alertType;
        }
    }
}
