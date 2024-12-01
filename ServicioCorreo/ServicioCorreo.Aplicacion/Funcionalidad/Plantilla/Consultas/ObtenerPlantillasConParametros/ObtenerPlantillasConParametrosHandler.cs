using MediatR;
using Microsoft.EntityFrameworkCore;
using ServicioCorreo.Aplicacion.Funcionalidad.Plantilla.Vm;
using ServicioCorreo.Dal.Datos.Context;
using ServicioCorreo.Dal.Utilitario.Plantilla;

namespace ServicioCorreo.Aplicacion.Funcionalidad.Plantilla.Consultas.ObtenerPlantillasConParametros
{
    public class ObtenerPlantillasConParametrosHandler : IRequestHandler<ObtenerPlantillasConParametrosConsulta, List<PlantillaVm>>
    {
        private readonly ContextCorreo _context;
        private readonly ParametroProcesador _parametroProcesador;
        public ObtenerPlantillasConParametrosHandler(ContextCorreo context, ParametroProcesador parametroProcesador)
        {
            _context = context;
            _parametroProcesador = parametroProcesador;
        }
        public async Task<List<PlantillaVm>> Handle(ObtenerPlantillasConParametrosConsulta request, CancellationToken cancellationToken)
        {
            var plantillas = await _context.Plantillas.ToListAsync();

            var plantillaVm =  plantillas.Select(plantilla =>
            {
                var parametros = _parametroProcesador.ObtenerParametrosDesdeContenido(plantilla.ContenidoHtml);
                return new PlantillaVm
                {
                    Id = plantilla.Id,
                    Nombre = plantilla.Nombre,
                    FechaCreacion = plantilla.FechaCreacion,
                    FechaUltimaModificacion = plantilla.FechaUltimaModificacion,
                    Parametros = parametros
                };
            }).ToList();

            return plantillaVm;
        }
    }
}
