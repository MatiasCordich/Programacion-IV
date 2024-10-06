using LogisticaContainers.Managers.Entidades.Auditoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaContainers.Managers.Entidades
{
    // Esto nos indica que la clase Container va a tener acceso a los metodos y propiedades de la clase Audit
    public class Container : Audit
    {
        public int IdContainer { get; set; }
        public string DescripcionContainer { get; set; }
        public int IdEstadoContainer { get; set; }
    }
}
