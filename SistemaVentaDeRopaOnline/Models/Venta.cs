using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SistemaVentaDeRopaOnline.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string MetodoPago { get; set; } = null!;

        [Required(ErrorMessage = "El correo es obligatorio")]
        public string Correo { get; set; } = null!;
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Apellido { get; set; } = null!;
        [Required(ErrorMessage = "La dirección es obligatorio")]
        public string Direccion { get; set; } = null!;

        [Required(ErrorMessage = "El DNI es obligatorio")]
        [StringLength(8, ErrorMessage = "El DNI debe ser de 8 dígitos")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El DNI debe contener solo números y tener 8 dígitos")]
        public string DNI { get; set; } = null!;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(9, ErrorMessage = "El teléfono debe ser de 9 dígitos")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El teléfono debe contener solo números y tener 9 dígitos")]
        public string Telefono { get; set; } = null!;

        public string TipoComprobante { get; set; } = null!;

        public int PedidoId { get; set; }
        [ForeignKey("PedidoId")]
        public Pedido? Pedido{ get; set; }
    }
}
