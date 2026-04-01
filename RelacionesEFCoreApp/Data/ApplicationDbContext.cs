using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RelacionesEFCoreApp.Models;

namespace RelacionesEFCoreApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 🔥 RELACIÓN 1:1
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Perfil)
                .WithOne(p => p.Usuario)
                .HasForeignKey<Perfil>(p => p.UsuarioId);
        }
    }
}