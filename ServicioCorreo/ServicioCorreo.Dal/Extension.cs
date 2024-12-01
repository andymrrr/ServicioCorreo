using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicioCorreo.Dal.Datos.Context;
using System.Text;

namespace ServicioCorreo.Dal
{
    public static class Extension
    {
        public static IServiceCollection AddServicioDatos(this IServiceCollection servicio, IConfiguration configuracion)
        {
          
            servicio.AddDbContext<ContextCorreo>(options =>
                options.UseSqlServer(configuracion.GetConnectionString("ServicioCorreo"),
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(ContextCorreo).Assembly.FullName)));

           


            return servicio;
        }
    }
}
