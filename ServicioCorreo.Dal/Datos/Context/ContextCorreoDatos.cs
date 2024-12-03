
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServicioCorreo.Dal.Modelo;
namespace ServicioCorreo.Dal.Datos.Context
{
    public class ContextCorreoDatos
    {
        public static async Task CargardatosAsincronos(ContextCorreo dbContext, ILoggerFactory logger)
        {
            try
            {
              
                if (!dbContext.Plantillas.Any())
                {

                    var PlantillaDatos = File.ReadAllText("../ServicioCorreo.Dal/CargaIniciar/Plantillas.json");
                    var plantillas = JsonConvert.DeserializeObject<List<Plantilla>>(PlantillaDatos);

                    if (plantillas != null)
                    {
                        await dbContext.Plantillas.AddRangeAsync(plantillas);
                        await dbContext.SaveChangesAsync();
                    }
                }
               



            }
            catch (Exception ex)
            {
                var log = logger.CreateLogger<ContextCorreoDatos>();
                log.LogError(ex.Message, ex.InnerException);
            }
        }
    }
}
