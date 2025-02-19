using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SistemaContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
 );
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<SistemaContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;  // Requiere al menos un número
    options.Password.RequiredLength = 6;   // Longitud mínima
    options.Password.RequireNonAlphanumeric = false; // Requiere un carácter especial
    options.Password.RequireUppercase = true; // Requiere una mayúscula
    options.Password.RequireLowercase = true; // Requiere una minúscula
    options.Password.RequiredUniqueChars = 1; // Número mínimo de caracteres únicos
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
