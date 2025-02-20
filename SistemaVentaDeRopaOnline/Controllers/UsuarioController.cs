using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Usuario> _userManager;
        public UsuarioController(RoleManager<IdentityRole> roleManager, UserManager<Usuario> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Listar()
        {
            var usuarios = _userManager.Users;
            return View(usuarios);
        }

        [HttpGet]
        public async Task<IActionResult> AsignarRoles(string idUsuario)
        {
            var usuario = await _userManager.FindByIdAsync(idUsuario);

            var roles = await _roleManager.Roles.ToListAsync();

            var rolesUsuario = await _userManager.GetRolesAsync(usuario);

            ViewBag.RolesDisponibles = new SelectList(roles, "Name", "Name");
            ViewBag.RolesAsignados = rolesUsuario;

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> AsignarRoles(string idUsuario, string rol)
        {
            var usuario = await _userManager.FindByIdAsync(idUsuario);
            
            var rolesUsuario = await _userManager.GetRolesAsync(usuario);

            if (rolesUsuario.Contains(rol))
            {
                CrearAlerta("error", "El usuario ya tiene este rol asignado.");
                return RedirectToAction("AsignarRoles", new { idUsuario });
            }

            var resultado = await _userManager.AddToRoleAsync(usuario, rol);
            if (!resultado.Succeeded)
            {
                CrearAlerta("error", "No se pudo asignar el rol.");
            }
            else
            {
                CrearAlerta("success", "Rol asignado correctamente.");
            }

            return RedirectToAction("AsignarRoles", new { idUsuario });
        }


        [HttpGet]
        public async Task<IActionResult> EliminarRolDeUsuario(string idUsuario, string rol)
        {
            var usuario = await _userManager.FindByIdAsync(idUsuario);

            var rolesUsuario = await _userManager.GetRolesAsync(usuario);
            if (rolesUsuario.Count == 1)
            {
                CrearAlerta("error", "No se puede eliminar el único rol del usuario.");
                return RedirectToAction("AsignarRoles", new { idUsuario });
            }

            var resultado = await _userManager.RemoveFromRoleAsync(usuario, rol);
            
            if (!resultado.Succeeded)
            {
                CrearAlerta("error", "No se pudo remover el rol");
            }
            else
            {
                CrearAlerta("success", "Se eliminó el rol correctamente");
            }

            return RedirectToAction("AsignarRoles", new { idUsuario });
        }

        public void CrearAlerta(string alertType, string alertMessage)
        {
            TempData["AlertMessage"] = alertMessage;
            TempData["AlertType"] = alertType;
        }

    }
}
