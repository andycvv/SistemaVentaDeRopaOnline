﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaVentaDeRopaOnline.Models;
using SistemaVentaDeRopaOnline.Models.ViewModels;

namespace SistemaVentaDeRopaOnline.Controllers
{
    public class SeguridadController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeguridadController
        (
            UserManager<Usuario> userManager, 
            SignInManager<Usuario> signInManager, 
            RoleManager<IdentityRole> roleManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Registrar()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario()
                {
                    UserName = model.Correo,
                    Email = model.Correo,
                    Nombre = model.Nombre
                };

                var resultado = await _userManager.CreateAsync(usuario, model.Password);

                if (resultado.Succeeded)
                {
                    await _userManager.AddToRoleAsync(usuario, "Cliente");

                    await _signInManager.SignInAsync(usuario, isPersistent: false);
                    return RedirectToAction("Index", "Producto");
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Ingresar()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ingresar(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(model.Correo, model.Password, model.Recordarme, false);

                if (resultado.Succeeded)
                {
                    var usuario = await _userManager.GetUserAsync(User);

                    CrearAlerta("success", "Sesión iniciada correctamente");

                    if(await _userManager.IsInRoleAsync(usuario, "Administrador"))
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }

                    return RedirectToAction("Index", "Producto");
                }

                CrearAlerta("error", "Correo o Clave incorrectos.");
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Ingresar");
        }

        public void CrearAlerta(string alertType, string alertMessage) 
        {
            TempData["AlertMessage"] = alertMessage;
            TempData["AlertType"] = alertType;
        } 
    }
}
