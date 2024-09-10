using Containers.WEB.Models;
using LogisticaContainers.Managers.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        // Dependencia del controlador que se inyectará mediante el constructor 
        private IContainerManager _containerManager;

        // Lista: Creamos una lista de contenedores en base a nuestro modelo de Contenedor creado. 
        private List<ContenedorVM> _contenedores { get; set; }

        // Constructor: Creamos el constructor del Controller
        // Constructor del controlador que recibe una instancia de IContainerManager a través de inyección de dependencias
        public ContenedorController(IContainerManager containerManager) {

            // Asigna la dependencia inyectada a la variable privada
            this._containerManager = containerManager;

            // Instanciamos una lista de contenedores
            _contenedores = new List<ContenedorVM>();

            // Agregamos dos objetos de tipo ContenedorVM de nuestro modelo
            _contenedores.Add(new ContenedorVM
            {
                IdContainer = 1, 
                FechaAlta = DateTime.Now,
                NumeroSerie = "ASDA-01",
                Direccion = "Avenida Siempreviva 2792"
            });

            _contenedores.Add(new ContenedorVM
            {
                IdContainer = 2,
                FechaAlta = DateTime.Now,
                NumeroSerie = "ASDAASFASD-22343",
                Direccion = "Calle Falsa 123"
            });
        }

        // GET: ContenedorController
        public ActionResult Index()
        {
            // Crea un nuevo contenedor utilizando el IContainerManager (inyección de dependencia)
            var contenedor = this._containerManager.CrearContainer();

            // Añade el contenedor creado a la lista _contenedores
            _contenedores.Add(new ContenedorVM
            {
                Direccion = contenedor.DescripcionContainer, // Asigna la descripción del contenedor a la dirección
                FechaAlta = contenedor.FechaAlta, // Asigna la fecha de alta del contenedor
                IdContainer = contenedor.IdContainer, // Asigna el ID del contenedor
                NumeroSerie = contenedor.DescripcionContainer // Usa la descripción del contenedor como número de serie
            });

            ContainerManager containerManager = new ContainerManager();

            // Retornamos en el Index la lista de contenedores
            return View(_contenedores);
        }

        // GET: ContenedorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContenedorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContenedorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContenedorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContenedorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContenedorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContenedorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
