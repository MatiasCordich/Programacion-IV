using LogisticaContainers.Managers.Entidades.Auditoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaContainers.Managers.Entidades
{
    public class Container : Audit
    {
        public int IdContainer { get; set; }
        public string DescripcionContainer { get; set; }
        public bool Estacargado { get; set; }
    }
}
