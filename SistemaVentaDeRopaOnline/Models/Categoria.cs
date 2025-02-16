using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentaDeRopaOnline.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ingrese el nombre de la categoría.")]
    [StringLength(30, ErrorMessage = "El nombre no debe superar los 30 caracteres.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "El nombre solo puede contener letras, números y espacios.")]
    public string Nombre { get; set; } = null!;

    public bool Estado { get; set; } = true;

    public List<Producto> Productos { get; set; } = new List<Producto>();
}