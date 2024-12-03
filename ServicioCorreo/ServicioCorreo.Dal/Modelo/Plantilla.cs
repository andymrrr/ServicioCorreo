using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioCorreo.Dal.Modelo
{
    public class Plantilla
    {
        public int Id { get; set; }  

        public string Nombre { get; set; }  

        public string ContenidoHtml { get; set; } 

        public string ParametrosEsperados { get; set; } 
        public DateTime FechaCreacion { get; set; }  

        public DateTime? FechaUltimaModificacion { get; set; }  

        public string Descripcion { get; set; }  

        public bool Activo { get; set; } = true;  
    }

}
