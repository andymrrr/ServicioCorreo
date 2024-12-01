
using System.Text.RegularExpressions;


namespace ServicioCorreo.Dal.Utilitario.Plantilla
{
    public class ParametroProcesador
    {
        public List<string> ObtenerParametrosDesdeContenido(string contenidoHtml)
        {
            // Regex para detectar {{nombreParametro}}
            var matches = Regex.Matches(contenidoHtml, @"{{(.*?)}}");
            return matches.Select(match => match.Groups[1].Value).Distinct().ToList();
        }
    }
}
