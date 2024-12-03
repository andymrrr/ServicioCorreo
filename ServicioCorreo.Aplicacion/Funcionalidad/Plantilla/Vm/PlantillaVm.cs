

namespace ServicioCorreo.Aplicacion.Funcionalidad.Plantilla.Vm
{
    public class PlantillaVm
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaUltimaModificacion { get; set; }
        public List<string> Parametros { get; set; }
    }
}
