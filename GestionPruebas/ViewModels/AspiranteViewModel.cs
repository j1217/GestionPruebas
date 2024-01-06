namespace GestionPruebas.ViewModels
{
    public class AspiranteViewModel
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DatosContacto { get; set; }
        public string EstadoPrueba { get; set; }
        public decimal Calificacion { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
