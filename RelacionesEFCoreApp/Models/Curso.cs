using System.ComponentModel.DataAnnotations;

namespace RelacionesEFCoreApp.Models
{
    public class Curso
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del curso es obligatorio.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Los créditos son obligatorios.")]
        [Range(1, 10, ErrorMessage = "Los créditos deben ser entre 1 y 10.")]
        public int Creditos { get; set; }

        // Navegación
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}