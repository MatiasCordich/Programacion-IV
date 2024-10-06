using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaContainers.Managers.ModelFactories
{
    public class ContainerCompleto
    {
        public int IdContainer { get; set; }
        public string DescripcionContainer { get; set; }
        public int IdEstadoContainer { get; set; }
        public string Estado { get; set; }
    }
}
