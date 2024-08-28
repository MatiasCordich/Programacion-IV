using LogisticaContainers.Managers.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaContainers.Managers.Managers
{
    // ------- El manager de Usuario va a contener toda la logica de negocio de Usuario ------- //
    public class UsuarioManager
    {
        public UsuarioManager() { }

        // ------- Este método se encarga de iniciar un nuevo viaje ------- //
        public void Iniciar()
        {
            Viaje viaje = new Viaje();
        }
    }
}
