using LogisticaContainers.Managers.Entidades.Auditoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaContainers.Managers.Entidades
{
    // Esto nos indica que la clase Hangar va a tener acceso a los metodos y propiedades de la clase Audit
    public class Hangar : Audit
    {
        public int IdHangar { get; set; }

        public string Nombre { get; set; }
    }
}
