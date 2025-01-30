using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentaDeRopaOnline.Models;

public class Talla
{
    public int Id { get; set; }

    [StringLength(30)]
    public string Nombre { get; set; } = null!;

    public List<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
    public List<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
