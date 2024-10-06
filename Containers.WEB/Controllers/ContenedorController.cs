using Containers.WEB.Models;
using LogisticaContainers.Managers.Entidades;
using LogisticaContainers.Managers.Managers;
using LogisticaContainers.Managers.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Containers.WEB.Controllers
{
    /*
     Este es nuestro CONTROLLER de ContenedoR. 
     Aca estaran todas los metodos del controler que nos permitiran gestionar el MODELO de Contenedor
     y la vista de Contenedor.
     */

    /*
     Cada metodo tiene comentado dos cosas
      - Metodo HTTP: Son metodos para hacer peticiones (GET, POST)
      - Ruta: La ruta a la que funciona dicho metodo del controlador  
     */


    public class ContenedorController : Controller
    {

        // Variable privada controlador donde se inyectará la dependencia mediante el constructor 
        private IContainerManager _containerManager;

        // Variable privada, donde utilizaremos los metodos del repositorio de EstadoContainer
        private IEstadoContainerRepository _estadoContainerRepository;

        // Constructor: Creamos el constructor del Controller
        // Constructor del controlador que recibe una instancia de IContainerManager a través de inyección de dependencias.
        // Recibe tambien la instancia del repositorio del EstadoContainer
        public ContenedorController(IContainerManager containerManager, IEstadoContainerRepository estadoContainerRepository)
        {

            _containerManager = containerManager;
            _estadoContainerRepository = estadoContainerRepository;
        }

        // GET: ContenedorController
        public ActionResult Index()
        {
            var containers = _containerManager.GetContainers();

            return View(containers);
        }

        // GET: ContenedorController/Details/5
        public ActionResult Details(int id)
        {
            var container = _containerManager.GetContainer(id);

            ContainerModel containerModel = new ContainerModel();
            containerModel.model = container;
            containerModel.ListaEstadosItem = new List<SelectListItem>();
            var estados = _estadoContainerRepository.GetEstadosContainer();
            foreach (var estado in estados)
            {
                containerModel.ListaEstadosItem.Add(new SelectListItem { Value = estado.IdEstadoContainer.ToString(), Text = estado.Descripcion });
            }

            return View(containerModel);
        }

        // GET: ContenedorController/Create
        public ActionResult Create()
        {
            ContainerModel containerModel = new ContainerModel();
            containerModel.model = null;
            containerModel.ListaEstadosItem = new List<SelectListItem>();
            var estados = _estadoContainerRepository.GetEstadosContainer();
            foreach (var estado in estados)
            {
                containerModel.ListaEstadosItem.Add(new SelectListItem { Value = estado.IdEstadoContainer.ToString(), Text = estado.Descripcion });
            }
            return View(containerModel);
        }

        // POST: ContenedorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Container container = new Container
                {
                    DescripcionContainer = collection["model.DescripcionContainer"],
                    IdEstadoContainer = int.Parse(collection["model.IdEstadoContainer"])
                };
                int idUsuario = 1;

                _containerManager.CrearContainer(container, idUsuario);

                // Una vez terminada la accion que me devuelva a la vista Index
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Caso contrari que me mantenga en la vista de crear y mostrara un mensaje de error de ser necesario
                return View();
            }
        }
        // GET: ContenedorController/Edit/5
        public ActionResult Edit(int id)
        {

            // Obtenemos el container especifico por su ID
            var container = _containerManager.GetContainer(id);

            // Obtenemos todos los estados posibles de un container
            var estados = _estadoContainerRepository.GetEstadosContainer();

            // Creamos el MODELO de Contanier
            ContainerModel containerModel = new ContainerModel();

            // El MODELO tiene de base el modelo que es el OBJETO de tipo Container, le pasamos el container obtenido
            containerModel.model = container;

            // Le pasamos al modelo la lista de seleccion de todos los estados obtenidos
            containerModel.ListaEstadosItem = new List<SelectListItem>();

            // Por cada estado quiero que me hagas un Item de seleccion cuyos valores sean Id del esatdo y la descripcion del estado. 
            foreach (var estado in estados)
            {
                containerModel.ListaEstadosItem.Add(new SelectListItem { Value = estado.IdEstadoContainer.ToString(), Text = estado.Descripcion });
            }

            return View(containerModel);

        }

        // POST: ContenedorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Container container = new Container
                {
                    DescripcionContainer = collection["model.DescripcionContainer"],
                    IdEstadoContainer = int.Parse(collection["model.IdEstadoContainer"])
                };
                int idUsuario = 1;

                _containerManager.ModificarContainer(id, container, idUsuario);

                // Una vez terminada la accion que me devuelva a la vista Index
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Caso contrario que me mantenga en la vista de editar y mostrara un mensaje de error de ser necesario
                return View();
            }

        }

        // GET: ContenedorController/Delete/5
        public ActionResult Delete(int id)
        {
            // Obtengo el Container a eliminar por su ID
            var container = _containerManager.GetContainer(id);

            // Obtengo todos los estados posibles que puede tener el Container
            var estados = _estadoContainerRepository.GetEstadosContainer();

            // Creamos el nuevo MODELO de Container
            ContainerModel containerModel = new ContainerModel();

            // Al modelo, en su propiedad model (de tipo Container) le paso el container obtenido
            containerModel.model = container;

            // Le pasamos al modelo la lista de seleccion de todos los estados obtenidos
            containerModel.ListaEstadosItem = new List<SelectListItem>();

            // Por cada estado quiero que me hagas un Item de seleccion cuyos valores sean Id del esatdo y la descripcion del estado. 
            foreach (var estado in estados)
            {
                containerModel.ListaEstadosItem.Add(new SelectListItem { Value = estado.IdEstadoContainer.ToString(), Text = estado.Descripcion });
            }

            return View(containerModel);

        }

        // POST: ContenedorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                int idUsuario = 1;

                // Llamo el metodo EliminarContainer dle manager y le paso el ID y el ID de usuario
                _containerManager.EliminarContainer(id, idUsuario);

                // Una vez terminada la accion que me devuelva a la vista Index
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Caso contrari que me mantenga en la vista de eliminar y mostrara un mensaje de error de ser necesario
                return View();
            }
        }
    }


}
