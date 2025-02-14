using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentaDeRopaOnline.Models;

public class Imagen
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Ingrese la url de la imagen.")]
    [StringLength(255, ErrorMessage = "La url no debe superar los 255 caracteres.")]
    public string Url { get; set; } = null!;
    public int ProductoId { get; set; }
    public Producto Producto { get; set; }
}
