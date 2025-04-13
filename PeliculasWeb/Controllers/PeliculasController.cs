using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PeliculasWeb.Models;
using PeliculasWeb.Models.ViewModels;
using PeliculasWeb.Repositorio.IRepositorio;
using PeliculasWeb.Utilidades;
using System.Collections.Generic;

namespace PeliculasWeb.Controllers
{
    public class PeliculasController : Controller
    {

        //instanciamos para poder acceder a todos los metodos

        private readonly IPeliculaRepositorio _repoPelicula; //lo llamo _repoPelicula
        private readonly ICategoriaRepositorio _repoCategoria; //lo llamo _repoCategoria
        public PeliculasController(IPeliculaRepositorio repoPelicula, ICategoriaRepositorio repoCategoria)
        {
            _repoPelicula = repoPelicula;
            _repoCategoria = repoCategoria;
        }

        public IActionResult Index()
        {
            return View(new Pelicula() { });
        }

        [HttpGet]

        //aca con este metodo llamamos al metodo de la api para que la api nos traiga la informacion que necesitamos para el front-end
        public async Task<IActionResult> GetTodasPeliculas()
        {
            return Json(new { data = await _repoPelicula.GetPeliculasTodoAsync(CT.RutaPeliculasApi) }); //el get no es generico para peliculas (no gettodoasync)
        }


        public async Task <IActionResult> Create()
        {
            //para crear una pelicula necesitamos asignarle una categoria:una lista desplegable con las categorias que hay.
            IEnumerable<Categoria> ctList = (IEnumerable<Categoria>) await _repoCategoria.GetTodoAsync(CT.RutaCategoriasApi); //casteo ienumerable

            //PeliculasVM es un ViewModel que combina dos cosas: una lista de categorías (listaCategorias)
            //para mostrar en un control desplegable y un objeto Pelicula que representa los datos de una película

            PeliculasVM objVM = new PeliculasVM()
            {

                //traigo la lista de categorias aca:

                ListaCategorias = ctList.Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Id.ToString()
                }),

                //traigo los datos de pelicula
                Pelicula = new Pelicula()
            };
            
            return View(objVM);
        }


    }
}
