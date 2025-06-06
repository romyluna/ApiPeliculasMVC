using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PeliculasWeb.Models;
using PeliculasWeb.Models.ViewModels;
using PeliculasWeb.Repositorio.IRepositorio;
using PeliculasWeb.Utilidades;
using System.Collections;
using System.Collections.Generic;

namespace PeliculasWeb.Controllers
{
    [Authorize]
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
            //return Json(new { data = await _repoPelicula.GetPeliculasTodoAsync(CT.RutaPeliculasApi) }); //el get no es generico para peliculas (no gettodoasync)

            var peliculaResponse = await _repoPelicula.GetPeliculasTodoAsync(CT.RutaPeliculasApi);
            return Json(new { data = peliculaResponse.Items });  // Solo la lista de películas


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

        [HttpPost]
        //para protegerse de ataques CSRF (Cross-Site Request Forgery).Esto ayuda a garantizar que las solicitudes POST
        //(como enviar formularios) sean legítimas y no generadas maliciosamente desde otro lugar
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pelicula pelicula)
        {
            //para crear una pelicula necesitamos asignarle una categoria:una lista desplegable con las categorias que hay.
            // Obtengo la lista de categorías desde un repositorio 
            IEnumerable<Categoria> ctList = (IEnumerable<Categoria>)await 
                _repoCategoria.GetTodoAsync(CT.RutaCategoriasApi); //casteo ienumerable

            //Creo un ViewModel (PeliculasVM) que combina la lista de categorías con los datos de la película
            PeliculasVM objVM = new PeliculasVM()
            {

                //traigo la lista de categorias aca:// Lista de categorías para mostrar en un dropdown

                ListaCategorias = ctList.Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Id.ToString()
                }),

                //traigo los datos de pelicula que se está creando (inicialmente vacía)
                Pelicula = new Pelicula()
            };

           // Compruebo si el modelo recibido es válido según las validaciones definidas en el modelo Pelicula
            if (ModelState.IsValid)
            {
                //con esto obtengo los archivos subidos desde el formulario 
                var files = HttpContext.Request.Form.Files;
                //si se ah subido algun archivo:
                if (files.Count > 0) 
                {
                    pelicula.Imagen = files[0];//asignar el iformFile directamente a la propiedad imagen
                }
                else
                {
                    // Si no se ha subido ningún archivo, retorno la vista con el ViewModel para que el usuario complete los datos
                    return View(objVM);
                }
                //Llamo al repositorio para crear la película en la API correspondiente
                await _repoPelicula.CrearPeliculaAsync(CT.RutaPeliculasApi,pelicula, HttpContext.Session.GetString("JWToken"));
                // Redirigir al usuario a la acción Index después de crear exitosamente la película
                return RedirectToAction(nameof(Index));
            }
            // Si el modelo no es válido, retorno la vista con el ViewModel para que el usuario corrija los errores
            return View(objVM);
        }

        //EDITAR:

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            //para crear una pelicula necesitamos asignarle una categoria:una lista desplegable con las categorias que hay.
            IEnumerable<Categoria> ctList = (IEnumerable<Categoria>)await _repoCategoria.GetTodoAsync(CT.RutaCategoriasApi); //casteo ienumerable

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

            if( id == null)
            {
                return NotFound();
            }

            //si se envia un id para mostrar los datos del formulario

            objVM.Pelicula = await _repoPelicula.GetAsync(CT.RutaPeliculasApi,id.GetValueOrDefault());


            if (objVM.Pelicula == null)
            {
                return NotFound();
            }
            return View(objVM);
        }
        
       

        [HttpPost]
        //para protegerse de ataques CSRF (Cross-Site Request Forgery).Esto ayuda a garantizar que las solicitudes POST
        //(como enviar formularios) sean legítimas y no generadas maliciosamente desde otro lugar
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Pelicula pelicula)
        {
            IEnumerable<Categoria> ctList = (IEnumerable<Categoria>)await _repoCategoria.GetTodoAsync(CT.RutaCategoriasApi);

            var objVM = new PeliculasVM()
            {
                ListaCategorias = ctList.Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Id.ToString()
                }),
                Pelicula = pelicula // Usar el objeto recibido en lugar de crear uno nuevo
            };

            if (!ModelState.IsValid)
            {
                return View("Edit", objVM);
            }

            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                pelicula.Imagen = files[0];
            }
            else
            {
                // Obtener la película existente para mantener la imagen actual
                var peliculaExistente = await _repoPelicula.GetAsync(CT.RutaPeliculasApi, pelicula.Id);
                if (peliculaExistente != null)
                {
                    pelicula.RutaIMagen = peliculaExistente.RutaIMagen;
                }
            }

            await _repoPelicula.ActualizarPeliculaAsync(CT.RutaPeliculasApi + pelicula.Id, pelicula, HttpContext.Session.GetString("JWToken"));
            return RedirectToAction(nameof(Index));
        
        }


        //BORRAR

        [HttpDelete]
        //para protegerse de ataques scsc
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _repoPelicula.BorrarAsync(CT.RutaPeliculasApi, id, HttpContext.Session.GetString("JWToken"));

            //si hay status si se pudo borrar 
            if (status)
            {
                return Json(new { success = true, message = "Borrado correctamente" });
            }

            return Json(new { success = true, message = "No se pudo borrar" });
        }

        //Busqueda

        [HttpGet]
        public async Task<IActionResult> GetPeliculasEnCategoria(int id)
        {
            return Json(new { data = await _repoPelicula.GetPeliculasEnCategoriaAsync(CT.RutaPeliculasEnCategoriaApi,id) });
        }

    }
}
