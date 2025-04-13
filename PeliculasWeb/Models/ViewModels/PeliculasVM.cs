using Microsoft.AspNetCore.Mvc.Rendering;

namespace PeliculasWeb.Models.ViewModels
{
    public class PeliculasVM
    {

        //trae la lista de categorias y los campos de la clase pelicula.
        //vmodel: Propósito: Combinar datos de diferentes modelos o fuentes y prepararlos para su representación en la vista.
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
        public Pelicula Pelicula { get; set; }
    }
}
