using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentaDeRopaOnline.Models;

public class DetallePedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int ProductoId { get; set; }
    public int TallaId { get; set; }
    public int ColorId { get; set; }
    public int Cantidad { get; set; }
    public double Precio { get; set; }
    public Color Color { get; set; } = null!;
    public Pedido Pedido { get; set; } = null!;
    public Producto Producto { get; set; } = null!;
    public Talla Talla { get; set; } = null!;
}
