


using ServicioCorreo.Dal.Datos.Context;
using ServicioCorreo.Dal.Datos.Interfaz;
using ServicioCorreo.Dal.Modelo;

namespace ServicioCorreo.Dal.Datos.Repositorio
{
    public class CorreoUoW : ICorreoUoW
    {
        private ContextCorreo _context { get; }

        // Repositorios
        public IRepositorio<Plantilla> Plantilla { get;  set; }
        public IRepositorio<Log> Log { get; set; }




        public CorreoUoW(ContextCorreo context)
        {
            _context = context;

            Plantilla = new Repositorio<Plantilla>(context);
            Log = new Repositorio<Log>(context);
          
        }

        
        public void GuardarCambios()
        {
            _context.SaveChanges();
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Método para liberar recursos
        public void Dispose()
        {
            _context.Dispose();
        }

        #region Transacciones
        public async Task BeginAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
        #endregion
    }
}

