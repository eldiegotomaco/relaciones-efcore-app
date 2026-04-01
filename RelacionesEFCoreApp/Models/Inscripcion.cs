using System.ComponentModel.DataAnnotations;

namespace RelacionesEFCoreApp.Models
{
    public class Inscripcion
    {
        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; } = null!;

        public int CursoId { get; set; }
        public Curso Curso { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de inscripción es requerida.")]
        [Display(Name = "Fecha de Inscripción")]
        public DateTime FechaInscripcion { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El estado es requerido.")]
        public string Estado { get; set; } = null!;
    }
}