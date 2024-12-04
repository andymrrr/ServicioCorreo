﻿using Microsoft.EntityFrameworkCore;
using ServicioCorreo.Dal.Datos.Paginacion.Interfaz;


namespace ServicioCorreo.Dal.Datos.Paginacion
{
    public class EvaluarPaginacion<T> where T : class
    {
        public static IQueryable<T> MostrarConsulta(IQueryable<T> consulta, IPaginacion<T> especificacion)
        {
            // Aplicar criterio de filtrado
            if (especificacion.Filtro != null)
            {
                consulta = consulta.Where(especificacion.Filtro);
            }

            // Aplicar ordenamiento
            if (especificacion.OrdenarPor != null)
            {
                consulta = especificacion.OrdenarDescendente
                    ? consulta.OrderByDescending(especificacion.OrdenarPor)
                    : consulta.OrderBy(especificacion.OrdenarPor);
            }

            // Aplicar paginación
            if (especificacion.HabilitarPaginacion)
            {
                consulta = consulta.Skip((especificacion.Pagina - 1) * especificacion.CantidadRegistro)
                                   .Take(especificacion.CantidadRegistro);
            }

            // Incluir relaciones
            if (especificacion.Inclusiones != null)
            {
                consulta = especificacion.Inclusiones.Aggregate(consulta,
                    (current, include) => current.Include(include));
            }

            return consulta.AsSplitQuery().AsNoTracking();
        }
    }


}