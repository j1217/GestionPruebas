namespace GestionPruebas.ViewModels
{
    public class PreguntaViewModel
    {
        public int ID { get; set; }
        public string TextoPregunta { get; set; }
        public string OpcionesRespuesta { get; set; }
        public string RespuestaCorrecta { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
