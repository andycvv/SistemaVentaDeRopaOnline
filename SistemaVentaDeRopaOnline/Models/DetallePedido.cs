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
    public int InventarioId { get; set; }
    public int Cantidad { get; set; }
    public double Precio { get; set; }
    public Pedido Pedido { get; set; } = null!;
    public Inventario Inventario { get; set; } = null!;
}
