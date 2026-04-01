using System.ComponentModel.DataAnnotations;

namespace RelacionesEFCoreApp.Models
{
    public class Estudiante
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del estudiante es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La carrera es obligatoria.")]
        public string Carrera { get; set; } = null!;

        // Navegación
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}