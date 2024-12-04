
using ServicioCorreo.Dal.Modelo;

namespace ServicioCorreo.Dal.Datos.Interfaz
{
    public interface ICorreoUoW : IDisposable
    {

        IRepositorio<Log> Log { get; set; }
        IRepositorio<Plantilla> Plantilla { get; set; }




        void GuardarCambios();
        Task GuardarCambiosAsync();

        Task BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }

}
