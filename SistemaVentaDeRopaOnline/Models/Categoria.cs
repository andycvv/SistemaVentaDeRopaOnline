using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentaDeRopaOnline.Models;

public class Categoria
{
    public int Id { get; set; }
    [StringLength(30)]
    public string Nombre { get; set; } = null!;
    public List<Producto> Productos { get; set; } = new List<Producto>();
}
