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
    [Range(1, 1000, ErrorMessage = "Ingrese un número válido para stock mayor a 0.")]
    public int Stock { get; set; } = 0;
    public Color? Color { get; set; } 
    public Producto? Producto { get; set; } 
    public Talla? Talla { get; set; } 
    public bool Estado { get; set; } = true;
    public List<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
}
