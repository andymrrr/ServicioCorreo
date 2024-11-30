
namespace ServicioCorreo.Servicios.Modelo
{
    public class ConfiguracionCorreo
    {
        public string ServidorSmtp { get; set; } 
        public int Puerto { get; set; } 
        public string CorreoRemitente { get; set; } 
        public string NombreRemitente { get; set; } 
        public string Contraseña { get; set; } 
        public bool UsarSsl { get; set; } 
    }
}
