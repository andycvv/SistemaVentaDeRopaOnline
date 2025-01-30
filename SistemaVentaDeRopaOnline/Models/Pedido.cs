using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentaDeRopaOnline.Models;

public class Pedido
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    [StringLength(50)]
    public string Estado { get; set; } = null!;
    public double Total { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public int? VentaId { get; set; }
    public Venta? Venta { get; set; }
    public List<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
}
