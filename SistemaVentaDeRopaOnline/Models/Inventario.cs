using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentaDeRopaOnline.Models;
public class Inventario
{
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public int TallaId { get; set; }
    public int ColorId { get; set; }
    public int Stock { get; set; }
    public Color Color { get; set; } = null!;
    public Producto Producto { get; set; } = null!;
    public Talla Talla { get; set; } = null!;
    public bool Estado { get; set; } = true;
    public List<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
}
