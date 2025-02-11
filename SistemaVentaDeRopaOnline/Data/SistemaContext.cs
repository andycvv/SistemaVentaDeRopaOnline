using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Data
{
    public class SistemaContext : DbContext
    {
        public SistemaContext(DbContextOptions<SistemaContext> options) : base(options)
        {
        }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Talla> Tallas { get; set; }
        public DbSet<Color> Colores { get; set; }
    }
}
