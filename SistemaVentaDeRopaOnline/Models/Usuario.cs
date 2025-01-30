using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentaDeRopaOnline.Models;

public class Usuario
{
    public int Id { get; set; }

    [StringLength(30)]
    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    public string Correo { get; set; } = null!;

    [StringLength(255)]
    public string Clave { get; set; } = null!;

    [StringLength(30)]
    public string Rol { get; set; } = null!;
    public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
