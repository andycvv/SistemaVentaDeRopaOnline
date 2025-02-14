using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentaDeRopaOnline.Models;

public class Usuario : IdentityUser
{
    [StringLength(30)]
    public string? Nombre { get; set; }
    public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
