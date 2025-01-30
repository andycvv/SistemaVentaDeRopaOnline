using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentaDeRopaOnline.Models;

public class Producto
{
    public int Id { get; set; }
    [StringLength(100)]
    public string Nombre { get; set; } = null!;
    public double Precio { get; set; }
    [StringLength(20)]
    public string Genero { get; set; } = null!;
    [StringLength(255)]
    public string Descripcion { get; set; } = null!;
    [StringLength(30)]
    public string Marca { get; set; } = null!;
    public DateTime FechaCreacion { get; set; }
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;
    public List<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
    public List<Imagen> ImagenProductos { get; set; } = new List<Imagen>();
    public List<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
