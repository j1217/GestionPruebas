namespace GestionPruebas.ViewModels
{
    public class UsuarioViewModel
    {
        public int ID { get; set; }
        public string NombreUsuario { get; set; }
        public string ContraseñaHash { get; set; }
        public string Rol { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
