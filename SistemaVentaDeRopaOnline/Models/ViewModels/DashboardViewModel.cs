namespace SistemaVentaDeRopaOnline.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalUsuarios { get; set; }
        public int TotalVentas { get; set; }
        public int TotalProductos { get; set; }
        public int TotalPedidos { get; set; }
        public double GananciasPendientes { get; set; }
        public double GananciasConfirmadas { get; set; }
        public List<Producto> UltimosProductosCreados { get; set; } = new List<Producto>();
    }
}
