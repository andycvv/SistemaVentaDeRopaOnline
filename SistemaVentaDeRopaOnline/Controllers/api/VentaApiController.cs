using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models.DTO;

namespace SistemaVentaDeRopaOnline.Controllers.api
{
    [Route("api/venta")]
    [ApiController]
    public class VentaApiController : ControllerBase
    {
        private readonly SistemaContext _sistemaContext;
        public VentaApiController(SistemaContext sistemaContext)
        {
            _sistemaContext = sistemaContext;
        }

        [HttpGet]
        public IActionResult GetVentas()
        {
            var ventas = _sistemaContext.Ventas
                .Select(v => new VentaDTO()
                {
                    Id = v.Id,
                    Fecha = v.Fecha,
                    MetodoPago = v.MetodoPago,
                    Correo = v.Correo,
                    Nombre = v.Nombre,
                    Apellido = v.Apellido,
                    Direccion = v.Direccion,
                    DNI = v.DNI,
                    Telefono = v.Telefono,
                    TipoComprobante = v.TipoComprobante,
                    PedidoId = v.PedidoId,
                });
            return Ok(ventas);
        }

        [HttpGet("{id}")]
        public IActionResult GetVenta(int id)
        {
            var venta = _sistemaContext.Ventas
                 .Select(v => new VentaDTO()
                 {
                     Id = v.Id,
                     Fecha = v.Fecha,
                     MetodoPago = v.MetodoPago,
                     Correo = v.Correo,
                     Nombre = v.Nombre,
                     Apellido = v.Apellido,
                     Direccion = v.Direccion,
                     DNI = v.DNI,
                     Telefono = v.Telefono,
                     TipoComprobante = v.TipoComprobante,
                     PedidoId = v.PedidoId,
                 })
                .FirstOrDefault(v => v.Id == id);
            if (venta == null)
            {
                return NotFound();
            }
            return Ok(venta);
        }

        [HttpGet("usuario")]
        public async Task<IActionResult> GetVentasPorUsuario(string nombre)
        {
            var ventas = await _sistemaContext.Ventas
                .Include(v => v.Pedido)
                    .ThenInclude(p => p.Usuario)
                .Where(v => v.Pedido.Usuario.Nombre.Contains(nombre))
                .Select(v => new VentaDTO() 
                { 
                    Id = v.Id,
                    Fecha = v.Fecha,
                    MetodoPago = v.MetodoPago,
                    Correo = v.Correo,
                    Nombre = v.Nombre,
                    Apellido = v.Apellido,
                    Direccion = v.Direccion,
                    DNI = v.DNI,
                    Telefono = v.Telefono,
                    TipoComprobante = v.TipoComprobante,
                    PedidoId = v.PedidoId,
                })
                .ToListAsync();

            return Ok(ventas);
        }
    }
}
