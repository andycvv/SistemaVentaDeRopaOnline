using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentaDeRopaOnline.Models;

public class Producto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Ingrese el nombre del producto.")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;
    [Range(0.01, double.MaxValue, ErrorMessage = "Ingrese un precio válido mayor a 0.00.")]
    public double Precio { get; set; } = 0.00;
    [StringLength(20)]
    public string Genero { get; set; } = null!;
    [Required(ErrorMessage = "Ingrese la descripción del producto.")]
    [StringLength(255)]
    public string Descripcion { get; set; } = null!;
    [Required(ErrorMessage = "Ingrese la marca del producto.")]
    [StringLength(30)]
    public string Marca { get; set; } = null!;
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public bool Estado { get; set; } = true;
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; } 
    public List<Imagen> ImagenProductos { get; set; } = new List<Imagen>();
    public List<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
