using LogisticaContainers.Managers.Entidades;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Containers.WEB.Models
{
    public class ContainerModel
    {
        public Container model { get; set; }
        public List<SelectListItem> ListaEstadosItem { get; set; }
    }
}
