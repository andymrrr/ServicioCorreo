

namespace ServicioCorreo.Dal.Utilitario.Correo
{
    public class PlantillaProcesador
    {
        public string ReemplazarParametros(string plantilla, Dictionary<string, string> parametros)
        {
            if (string.IsNullOrEmpty(plantilla))
                throw new ArgumentException("La plantilla no puede ser nula o vacía.", nameof(plantilla));

            if (parametros == null || !parametros.Any())
                return plantilla;

            foreach (var parametro in parametros)
            {
                plantilla = plantilla.Replace($"{{{{{parametro.Key}}}}}", parametro.Value);
            }
            return plantilla;
        }
    }
}
