using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicioCorreo.Dal.Utilitario.Correo;
using ServicioCorreo.Dal.Utilitario.Plantilla;
using System.Reflection;

namespace ServicioCorreo.Aplicacion
{
    public static class Extension
    {
        public static IServiceCollection AddServicioAplicacion(this IServiceCollection servicio, IConfiguration configuracion)
        {
            //Registrar MediaTR
            servicio.AddMediatR(opcion => opcion.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            servicio.AddTransient<ParametroProcesador>();
            servicio.AddTransient<PlantillaProcesador>();



            return servicio;
        }
    }
}
