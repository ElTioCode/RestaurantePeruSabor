using Microsoft.EntityFrameworkCore;
using RestaurantePeruSabor.Data.Entities;

namespace RestaurantePeruSabor.Data
{
    public class RestauranteContext : DbContext
    {
        public DbSet<UsuarioEntity> Usuarios { get; set; }
        public DbSet<ClienteEntity> Clientes { get; set; }
        public DbSet<PedidoEntity> Pedidos { get; set; }
        public DbSet<DetallePedidoEntity> DetallePedidos { get; set; }
        public DbSet<VentaEntity> Ventas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
                @"Server=DESKTOP-BG72TB3\SQLEXPRESS;Database=RestaurantePeruSabor;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VentaEntity>()
                .HasOne(v => v.Pedido)
                .WithMany()
                .HasForeignKey(v => v.PedidoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VentaEntity>()
                .HasOne(v => v.Cliente)
                .WithMany()
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}