using Microsoft.EntityFrameworkCore;
using HousesSytem.Models;

namespace HousesSytem.App
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Piso> Pisos { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<Reservacion> Reservaciones { get; set; }
        public DbSet<DashboardViewModel> Dashboard { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones adicionales para las entidades y relaciones

            modelBuilder.Entity<Categoria>()
               .HasMany(c => c.Habitaciones)
               .WithOne(h => h.Categoria)
               .HasForeignKey(h => h.CategoriaId);

            modelBuilder.Entity<Piso>()
                .HasMany(p => p.Habitaciones)
                .WithOne(h => h.Piso)
                .HasForeignKey(h => h.PisoId);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Habitacion)
                .WithMany(h => h.Clientes)
                .HasForeignKey(c => c.HabitacionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Habitacion>()
                .HasMany(h => h.Reservaciones)
                .WithOne(r => r.Habitacion)
                .HasForeignKey(r => r.HabitacionId);
            modelBuilder.Entity<Reservacion>()
                .HasOne(r => r.Habitacion)
                .WithMany(h => h.Reservaciones)
                .HasForeignKey(r => r.HabitacionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DashboardViewModel>()
            .HasKey(d => d.Id);
        }
    }

}
