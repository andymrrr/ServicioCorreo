

namespace ServicioCorreo.Dal.Modelo
{
    public class Log
    {
        public int Id { get; set; } 
        public string Destinatario { get; set; } 
        public string Asunto { get; set; } 
        public bool Exito { get; set; } 
        public string Error { get; set; } 
        public DateTime FechaEnvio { get; set; } 
    }
}
