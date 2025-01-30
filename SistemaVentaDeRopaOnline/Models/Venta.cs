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
        [StringLength(50)]
        public string MetodoPago { get; set; } = null!;
        public int PedidoId { get; set; }
        [ForeignKey("PedidoId")]
        public Pedido Pedido{ get; set; } = null!;
    }
}
