using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaVentaDeRopaOnline.Models
{
    public class Color
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(30, ErrorMessage = "El nombre no puede superar los 30 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El código es obligatorio.")]
        [StringLength(7, ErrorMessage = "El código no puede superar los 7 caracteres.")]
        public string Codigo { get; set; } = null!;

        public bool Estado { get; set; } = true;

        public List<Inventario> Inventarios { get; set; } = new List<Inventario>();
    }
}