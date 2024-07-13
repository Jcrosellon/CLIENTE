using Microsoft.EntityFrameworkCore;
using CLIENTE.Models;

namespace CLIENTE.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuraciones adicionales...
        }
    }
}
