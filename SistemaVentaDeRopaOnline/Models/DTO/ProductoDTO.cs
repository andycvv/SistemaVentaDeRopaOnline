using System.ComponentModel.DataAnnotations;

namespace SistemaVentaDeRopaOnline.Models.DTO
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public double Precio { get; set; } = 0.00;
        public string Genero { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public bool Estado { get; set; } = true;
        public int CategoriaId { get; set; }
        public int StockTotal { get; set; }
        public int TotalVendidos { get; set; }
    }
}
