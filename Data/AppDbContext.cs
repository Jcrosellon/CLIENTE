using Microsoft.EntityFrameworkCore;
using CLIENTE.Models;

namespace CLIENTE.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar las relaciones entre Pedido y Cliente
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.Cliente_id)
                .OnDelete(DeleteBehavior.Restrict); // Asegura que no se eliminen en cascada si no está configurado en la base de datos

            base.OnModelCreating(modelBuilder);
        }
    }
}
