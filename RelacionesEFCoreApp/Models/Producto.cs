using System.ComponentModel.DataAnnotations;

namespace RelacionesEFCoreApp.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public List<DetallePedido> Detalles { get; set; } = new();
    }
}