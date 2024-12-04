
using MediatR;
using ServicioCorreo.Aplicacion.Funcionalidad.Correos.Comando.EnviarCorreos;
using ServicioCorreo.Aplicacion.Funcionalidad.Plantilla.Vm;
using ServicioCorreo.Aplicacion.Paginacion.Plantillas;
using ServicioCorreo.Dal.Datos.Interfaz;
using ServicioCorreo.Dal.Datos.Paginacion.Modelo;
using ServicioCorreo.Dal.Utilitario.Plantilla;


namespace ServicioCorreo.Aplicacion.Funcionalidad.Plantilla.Consultas.PaginacionPlantillas
{
    public class PaginacionPlantillasHandler : IRequestHandler<PaginacionPlantillasConsulta, PaginacionVm<PlantillaVm>>
    {
        private readonly ICorreoUoW _contex;
        private readonly ParametroProcesador _parametroProcesador;

        public PaginacionPlantillasHandler(ICorreoUoW contex, ParametroProcesador parametroProcesador)
        {

            _contex = contex;
            _parametroProcesador = parametroProcesador;

        }
        public async Task<PaginacionVm<PlantillaVm>> Handle(PaginacionPlantillasConsulta request, CancellationToken cancellationToken)
        {
            var paginacionParametro = new PaginacionPalntillaParametro
            {
                Pagina= request.Pagina,
                CantidadRegistroPorPagina = request.CantidadRegistroPorPagina,
                Busqueda = request.Busqueda,
                Ordenar = request.Ordenar,
                PlantillaId = request.PlantillaId,
                Nombre = request.Nombre
            };

            var especificaciones = new PaginacionPlantilla(paginacionParametro);
            var plantillas = await _contex.Plantilla.BuscarTodaEspecificificaciones(especificaciones);

            var cantidadEspecificaciones = new PaginacionParaConteoDePalntilla(paginacionParametro);
            var totalplantillas = await _contex.Plantilla.CantidadAsincrona(cantidadEspecificaciones);

            var correosComandos = plantillas.Select(plantilla =>
            {
                // Extrae los parámetros desde el contenido HTML de la plantilla
                var parametros = _parametroProcesador.ObtenerParametrosDesdeContenido(plantilla.ContenidoHtml);

                // Crea un diccionario con los nombres de parámetros como claves y valores predeterminados
                var parametrosDict = parametros.ToDictionary(param => param, param => string.Empty);

                // Construye el comando EnviarCorreosComando
                return new PlantillaVm
                {
                    IdPlantilla = plantilla.Id,
                    Asunto = $"Correo basado en {plantilla.Nombre}", // Puedes modificar esto según sea necesario
                    Parametros = parametrosDict
                };
            }).ToList();
            IReadOnlyList<PlantillaVm> readOnlyLista = correosComandos.AsReadOnly();

            return new PaginacionVm<PlantillaVm>
            {
                TotalRegistros = totalplantillas,
                PaginaActual = request.Pagina,
                CantidadRegistroPorPagina = request.CantidadRegistroPorPagina,
                Datos = readOnlyLista
            };
        }


    }
}
