using LogisticaContainers.Managers.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaContainers.Managers.Managers
{

    // ------- Creamos la Interfaz del Manager de Container ------- //
    // Esta interfaz va a definir todas las funcionalidades (metodo) del manager
    public interface IContainerManager
    {
        Container CrearContainer();
    }

    // ------- El manager de Container va a contener toda la logica de negocio de Container ------- //
    // Implementamos nuestra interfaz
    public class ContainerManager : IContainerManager
    {
        public ContainerManager() { }

        // ------- Este método se encarga de crear el Container ------- //
        // Le damos un cuerpo logico a la funcion implementada de la interfaz IContainerManager
        public Container CrearContainer()
        {
            Container container = new Container
            {
                IdContainer = 3,
                DescripcionContainer = "ASD-QE-12",
                Estacargado = false,
                IdUsuarioAlta = 1,
                FechaAlta = DateTime.Now
            };

            return container;
        }

    }
}
