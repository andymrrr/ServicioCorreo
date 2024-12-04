

namespace ServicioCorreo.Aplicacion.Funcionalidad.Plantilla.Vm
{
    public class PlantillaVm
    {
        public string Destinatario { get; set; }
        public string Asunto { get; set; }
        public int? IdPlantilla { get; set; }
        public string? CuerpoHtmlPersonalizado { get; set; }
        public Dictionary<string, string> Parametros { get; set; } = new Dictionary<string, string>();
        public List<Dictionary<string, object>>? Items { get; set; } = new List<Dictionary<string, object>>();
    }
}
