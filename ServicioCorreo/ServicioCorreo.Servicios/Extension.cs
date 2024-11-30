
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicioCorreo.Servicios.Interffaz;
using ServicioCorreo.Servicios.Modelo;
using ServicioCorreo.Servicios.Repositorios;

namespace ServicioCorreo.Servicios
{
    public static class Extension
    {
        public static IServiceCollection AddServicio(this IServiceCollection servicio, IConfiguration configuracion)
        {

            servicio.Configure<ConfiguracionCorreo>(configuracion.GetSection("ConfiguracionCorreo"));
            servicio.AddScoped<IServicioCorreos, ServicioCorreos>();


            return servicio;
        }

    }
}
