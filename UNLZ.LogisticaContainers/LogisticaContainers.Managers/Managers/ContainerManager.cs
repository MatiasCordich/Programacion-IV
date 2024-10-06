using LogisticaContainers.Managers.Entidades;
using LogisticaContainers.Managers.ModelFactories;
using LogisticaContainers.Managers.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaContainers.Managers.Managers
{

    // ----- Interfaz del manager de Container para la inyección de dependencias ----- //
    public interface IContainerManager
    {
        // Métodos CRUD abstractos de la interfaz //

        // ---- Obtener una lista de Containers ---- //
        IEnumerable<ContainerCompleto> GetContainers();

        // ---- Obtener un Container ---- //
        Container GetContainer(int IdContainer);

        // ---- Crear un Container ---- //
        int CrearContainer(Container container, int IdUsuarioAlta);

        // ---- Modificar un Container ---- //
        bool ModificarContainer(int IdContainer, Container container, int IdUsuarioModificacion);

        // ---- Eliminar un Container ---- //
        bool EliminarContainer(int IdContainer, int IdUsuarioBaja);
    }

    // ------- El manager de Container va a contener toda la logica de negocio de Container ------- //
    // Implementamos nuestra interfaz
    public class ContainerManager : IContainerManager
    {
        // ---- Propiedad: Repositorio del Container ---- //
        private IContainerRepository _repo;

        // ---- Constructor ---- //
        public ContainerManager(IContainerRepository repo)
        {
            // Una vez instanciado el manager le pasamos el repositorio
            _repo = repo;
        }

        // ---- Implementación de los métodos de la interfaz ---- //

        /// <summary>
        /// Obtiene un modelo (objeto) de Container por Id 
        /// </summary>
        /// <param name="IdContainer">Id del Contenedor</param>
        /// <returns>Devuelve un Container</returns>
        public Container GetContainer(int IdContainer)
        {
            var container = _repo.GetContainer(IdContainer);
            return container;
        }

        /// <summary>
        /// Obtiene una lista de Containers
        /// </summary>
        /// <returns>Devuelve una lista de Containers</returns>
        public IEnumerable<ContainerCompleto> GetContainers()
        {
            return _repo.GetContainersCompleto();
        }

        /// <summary>
        /// Crea un Container en la Base de Datos
        /// </summary>
        /// <param name="containerVm">Datos del contenedor</param>
        /// <param name="IdUsuarioAlta">Id del usuario de la acción</param>
        /// <returns>Devuelve el ID del Container creado</returns>
        public int CrearContainer(Container container, int IdUsuarioAlta)
        {
            container.IdUsuarioAlta = IdUsuarioAlta;
            container.FechaAlta = DateTime.Now;
            var cont = _repo.CrearContainer(container);

            return cont;
        }

        /// <summary>
        /// Modifica los datos de un container a partir de un Id por los que se envían en el MODELO de Container
        /// </summary>
        /// <param name="IdContainer">Id del container a modificar</param>
        /// <param name="containerVm">Datos del contenedor</param>
        /// <param name="IdUsuarioModificacion">Id del usuario que hace la acción</param>
        /// <returns>Devuelve un booleano. True: Si logro hacer la modificacion, False: Hubo un error</returns>
        public bool ModificarContainer(int IdContainer, Container container, int IdUsuarioModificacion)
        {
            //Obtengo lo que viene de la base de datos
            var containerEnDb = _repo.GetContainer(IdContainer);

            //En el objeto que viene de la base de datos, le "pego" los valores que me vienen del formulario
            containerEnDb.DescripcionContainer = container.DescripcionContainer;
            containerEnDb.IdEstadoContainer = container.IdEstadoContainer;
            containerEnDb.IdUsuarioModificacion = IdUsuarioModificacion;
            containerEnDb.FechaModificacion = DateTime.Now;
            var cont = _repo.ModificarContainer(IdContainer, containerEnDb);

            return cont;
        }

        /// <summary>
        /// Elimina un container
        /// </summary>
        /// <param name="IdContainer">Id del Contenedor</param>
        /// <param name="IdUsuarioBaja">Id del usuario de la acción</param>
        /// <returns>Devuelve un booleano. True: Si logro hacer la modificacion, False: Hubo un error</returns>

        public bool EliminarContainer(int IdContainer, int IdUsuarioBaja)
        {
            return _repo.EliminarContainer(IdContainer, IdUsuarioBaja);
        }

    }
}
