using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RelacionesEFCoreApp.Models;

namespace RelacionesEFCoreApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 🔹 Tablas originales
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }

        // 🔹 Nuevas tablas
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔥 RELACIÓN 1:1 (Usuario - Perfil)
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Perfil)
                .WithOne(p => p.Usuario)
                .HasForeignKey<Perfil>(p => p.UsuarioId);

            // 🔥 RELACIÓN N:M (Estudiante - Curso)
            modelBuilder.Entity<Inscripcion>()
                .HasKey(i => new { i.EstudianteId, i.CursoId });

            modelBuilder.Entity<Inscripcion>()
                .HasOne(i => i.Estudiante)
                .WithMany(e => e.Inscripciones)
                .HasForeignKey(i => i.EstudianteId);

            modelBuilder.Entity<Inscripcion>()
                .HasOne(i => i.Curso)
                .WithMany(c => c.Inscripciones)
                .HasForeignKey(i => i.CursoId);
        }
    }
}