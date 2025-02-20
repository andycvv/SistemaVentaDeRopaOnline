using Microsoft.AspNetCore.Identity;
using SistemaVentaDeRopaOnline.Models;
using System;
using System.Threading.Tasks;

public class DbInitializer
{
    public static async Task SeedData(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Administrador", "Cliente", "Vendedor" };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        await CreateUserIfNotExists(userManager, "admin1", "admin1@example.com", "Admin123!", "Administrador");
        await CreateUserIfNotExists(userManager, "admin2", "admin2@example.com", "Admin123!", "Administrador");

        await CreateUserIfNotExists(userManager, "cliente1", "cliente@example.com", "Cliente123!", "Cliente");
    }

    private static async Task CreateUserIfNotExists(UserManager<Usuario> userManager, string name, string email, string password, string role)
    {
        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new Usuario
            {
                UserName = email,
                Nombre = name,
                Email = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }

}
