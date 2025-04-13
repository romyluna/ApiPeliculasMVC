using Microsoft.AspNetCore.Mvc;
using PeliculasWeb.Models;
using PeliculasWeb.Repositorio.IRepositorio;
using PeliculasWeb.Utilidades;

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




    }
}
