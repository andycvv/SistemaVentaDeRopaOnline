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
    options.Password.RequireDigit = true;  // Requiere al menos un n�mero
    options.Password.RequiredLength = 6;   // Longitud m�nima
    options.Password.RequireNonAlphanumeric = false; // Requiere un car�cter especial
    options.Password.RequireUppercase = true; // Requiere una may�scula
    options.Password.RequireLowercase = true; // Requiere una min�scula
    options.Password.RequiredUniqueChars = 1; // N�mero m�nimo de caracteres �nicos
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
    pattern: "{controller=Talla}/{action=Listar}/{id?}")
    .WithStaticAssets();


app.Run();
