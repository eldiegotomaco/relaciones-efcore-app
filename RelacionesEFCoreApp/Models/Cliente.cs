using System.ComponentModel.DataAnnotations;

namespace RelacionesEFCoreApp.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string? Nombre { get; set; }

        public string? Email { get; set; }

        // 🔥 Relación 1:N
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}