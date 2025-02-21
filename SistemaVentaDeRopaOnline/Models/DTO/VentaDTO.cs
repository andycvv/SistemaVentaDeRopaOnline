using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaVentaDeRopaOnline.Models.DTO
{
    public class VentaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string MetodoPago { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string DNI { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string TipoComprobante { get; set; } = null!;
        public int PedidoId { get; set; }
    }
}
