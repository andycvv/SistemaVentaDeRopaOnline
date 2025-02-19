using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;
using SistemaVentaDeRopaOnline.Models.DTO;

namespace SistemaVentaDeRopaOnline.Controllers.api
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoApiController : ControllerBase
    {
        private readonly SistemaContext _sistemaContext;
        public ProductoApiController(SistemaContext sistemaContext)
        {
            _sistemaContext = sistemaContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var products = await _sistemaContext.Productos
                .Select(p => new ProductoDTO()
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Genero = p.Genero,
                    Descripcion = p.Descripcion,
                    Marca = p.Marca,
                    Estado = p.Estado,
                    CategoriaId = p.CategoriaId,
                    StockTotal = p.Inventarios.Sum(i => i.Stock),
                    TotalVendidos = p.Inventarios
                        .SelectMany(i => i.DetallePedidos)
                        .Where(d => d.Pedido.Estado == "Pagado")
                        .Sum(d => d.Cantidad)
                })
                .ToListAsync();
            return Ok(products);
        }

        [HttpGet("/stock-bajo")]
        public async Task<IActionResult> GetProductsStockBajo(int stock)
        {
            var products = await _sistemaContext.Productos
                .Where(p => p.Inventarios.Sum(i => i.Stock) < stock)
                .Select(p => new ProductoDTO()
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Genero = p.Genero,
                    Descripcion = p.Descripcion,
                    Marca = p.Marca,
                    Estado = p.Estado,
                    CategoriaId = p.CategoriaId,
                    StockTotal = p.Inventarios.Sum(i => i.Stock),
                    TotalVendidos = p.Inventarios
                        .SelectMany(i => i.DetallePedidos)
                        .Where(d => d.Pedido.Estado == "Pagado")
                        .Sum(d => d.Cantidad)
                })
                .ToListAsync();
            return Ok(products);
        }

        [HttpGet("/producto-por-categoria")]
        public async Task<IActionResult> GetProductsPorCategoria(string categoria)
        {
            var products = await _sistemaContext.Productos
            .Where(p => p.Categoria.Nombre == categoria)
            .Select(p => new ProductoDTO()
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Genero = p.Genero,
                Descripcion = p.Descripcion,
                Marca = p.Marca,
                Estado = p.Estado,
                CategoriaId = p.CategoriaId,
                StockTotal = p.Inventarios.Sum(i => i.Stock),
                TotalVendidos = p.Inventarios
                        .SelectMany(i => i.DetallePedidos)
                        .Where(d => d.Pedido.Estado == "Pagado")
                        .Sum(d => d.Cantidad)
            })
            .ToListAsync();

            return Ok(products);
        }

        [HttpGet("/productos-mas-vendidos")]
        public async Task<IActionResult> GetProductsMasVendidos(int topProductos)
        {
            var products = await _sistemaContext.Productos
                .OrderByDescending(p => p.Inventarios
                    .SelectMany(i => i.DetallePedidos)
                    .Where(d => d.Pedido.Estado == "Pagado")
                    .Sum(d => d.Cantidad))
                .Take(topProductos)
                .Select(p => new ProductoDTO()
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Genero = p.Genero,
                    Descripcion = p.Descripcion,
                    Marca = p.Marca,
                    Estado = p.Estado,
                    CategoriaId = p.CategoriaId,
                    StockTotal = p.Inventarios.Sum(i => i.Stock),
                    TotalVendidos = p.Inventarios
                        .SelectMany(i => i.DetallePedidos)
                        .Where(d => d.Pedido.Estado == "Pagado")
                        .Sum(d => d.Cantidad)
                })
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("/productos-buscar-por-nombre")]
        public async Task<IActionResult> GetProductsBuscarPorNombre(string nombre)
        {
            var products = await _sistemaContext.Productos
                .Where(p => p.Nombre.Contains(nombre))
                .Select(p => new ProductoDTO()
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Genero = p.Genero,
                    Descripcion = p.Descripcion,
                    Marca = p.Marca,
                    Estado = p.Estado,
                    CategoriaId = p.CategoriaId,
                    StockTotal = p.Inventarios.Sum(i => i.Stock),
                    TotalVendidos = p.Inventarios
                        .SelectMany(i => i.DetallePedidos)
                        .Where(d => d.Pedido.Estado == "Pagado")
                        .Sum(d => d.Cantidad)
                })
                .ToListAsync();

            return Ok(products);
        }

    }
}
