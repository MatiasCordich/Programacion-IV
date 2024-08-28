using LogisticaContainers.Managers.Managers;
using LogisticaContainers.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Diagnostics;
using Container = LogisticaContainers.Managers.Entidades.Container;

namespace LogisticaContainers.Web.Controllers
{
    // ------- Controlador de Home: Practicamente va a contner los metodos que van a funcionar como intermediarios entre los modelos y las vistas ------- //
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // ------- Cada método del controlador debe tener el nombre de su vista correspondiente ------- //
        public IActionResult Index()
        {
            // ------- En la vista Index vamos a crear el Container y plasmar sus datos en la vista ------- //

            // Manager: Creamos el manager de Container que tiene la logica de negocio
            ContainerManager manager = new ContainerManager();

            // Container: Creamos el container mediane el método del manager CrearContainer()
            // Nos devuelve el objeto Container creado y lo guardamos en una variable del mismo tipo
            Container container = manager.CrearContainer();

            // ViewBag: Podemos intercambiar datos entre el controlador y la vista sin necesidad de definir un tipo de datos específico
            // dataContainer es el nombre que le damos nostoros
            // Recibira como valor el objeto container
            ViewBag.dataContainer = container;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
