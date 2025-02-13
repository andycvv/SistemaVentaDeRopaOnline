using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentaDeRopaOnline.Models;

public class Talla
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ingrese el nombre de la talla.")]
    [StringLength(30, ErrorMessage = "El nombre no debe superar los 30 caracteres.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "El nombre solo puede contener letras, números y espacios.")]
    public string Nombre { get; set; } = null!;
    public bool Estado { get; set; } = true;

    public List<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
