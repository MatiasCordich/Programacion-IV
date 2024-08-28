using LogisticaContainers.Managers.Entidades.Auditoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaContainers.Managers.Entidades
{
    // Esto nos indica que la clase Viaje va a tener acceso a los metodos y propiedades de la clase Audit
    public class Viaje : Audit
    {
        public int IdViaje { get; set; }
        public int IdContainer { get; set; }
        public int IdHangarOrigen { get; set; }
        public int IdHangarDestino { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; } // Agregando un signo de interrogacion indico que ese valor puede ser NULO
    }
}
