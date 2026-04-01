using System.ComponentModel.DataAnnotations;

namespace RelacionesEFCoreApp.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        public DateTime FechaPedido { get; set; }

        public decimal Total { get; set; }

        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; } // 🔥 ESTA LÍNEA ES LA CLAVE

        public List<DetallePedido> Detalles { get; set; } = new();
    }
}