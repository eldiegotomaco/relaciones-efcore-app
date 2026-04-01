namespace RelacionesEFCoreApp.Models
{
    public class Perfil
    {
        public int Id { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
