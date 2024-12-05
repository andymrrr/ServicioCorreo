using ServicioCorreo.Dal.Datos.Paginacion.Interfaz;
using System.Linq.Expressions;


namespace ServicioCorreo.Dal.Datos.Paginacion
{
    public class PaginacionBase<T> : IPaginacion<T>
    {
        public PaginacionBase() { }

        public PaginacionBase(Expression<Func<T, bool>> criterio)
        {
            Filtro = criterio;
        }

        public Expression<Func<T, bool>>? Filtro { get; }
        public List<Expression<Func<T, object>>> Inclusiones { get; } = new();
        public Expression<Func<T, object>>? OrdenarPor { get; private set; }
        public bool OrdenarDescendente { get; private set; } // Ahora es un booleano en lugar de expresión

        public int Pagina { get; private set; }
        public int CantidadRegistro { get; private set; }
        public bool HabilitarPaginacion { get; private set; }

        protected void AgregarOrdenarPor(Expression<Func<T, object>> ordenarExpresion)
        {
            OrdenarPor = ordenarExpresion;
            OrdenarDescendente = false; 
        }

        protected void AgregarOrdenarDescendiente(Expression<Func<T, object>> ordenarExpresion)
        {
            OrdenarPor = ordenarExpresion;
            OrdenarDescendente = true; 
        }

        protected void AplicarPaginacion(int pagina, int cantidadRegistro)
        {
            Pagina = pagina;
            CantidadRegistro = cantidadRegistro;
            HabilitarPaginacion = true;
        }

        protected void AgregarIncluir(Expression<Func<T, object>> incluirExpresion)
        {
            Inclusiones.Add(incluirExpresion);
        }
    }

}
