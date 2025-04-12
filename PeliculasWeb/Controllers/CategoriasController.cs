using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using PeliculasWeb.Models;
using PeliculasWeb.Repositorio.IRepositorio;
using PeliculasWeb.Utilidades;

namespace PeliculasWeb.Controllers
{
    public class CategoriasController : Controller
    {

        //instanciamos para poder acceder a todos los metodos

        private readonly ICategoriaRepositorio _repoCategoria; //lo llamo _repoCategoria

        public CategoriasController(ICategoriaRepositorio repoCategoria)
        {
            _repoCategoria = repoCategoria;
        }
        public IActionResult Index()
        {
            return View(new Categoria() { });

        }

        [HttpGet]

        //aca con este metodo llamamos al metodo de la api para que la api nos traiga la informacion que necesitamos para el front-end
        public async Task <IActionResult> GetTodasCategorias()
        {
            return Json(new { data = await _repoCategoria.GetTodoAsync(CT.RutaCategoriasApi) });
        }

        [HttpGet]
        public IActionResult Create() //el nombre create viene del index.cshtml = asp-action="Create" tiene que tener el mismo nombre
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create(Categoria categoria) //objeto categoria llamado categoria que viene del create.cshtml
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repoCategoria.CrearAsync(CT.RutaCategoriasApi, categoria);//le manda a la api la url y el objeto categoria del formulario
                    return RedirectToAction(nameof(Index));
                }

                return View(categoria);
            }
            catch (Exception ex)
            {
                throw ;
            }

        }


        //EDIT

        [HttpGet]
        public async Task<IActionResult> Edit(int? id) //el nombre create viene del index.cshtml = asp-action="Create" tiene que tener el mismo nombre
        {
            //Creamos una nueva instancia de la clase Categoria.
            //Esto es para preparar un objeto que eventualmente se llenará con los datos de la categoría que queremos editar
            Categoria itemCategoria = new Categoria();
            if (id == null)
            {
                return NotFound();
            }
            //id.GetValueOrDefault() = si tiene un numero de id le pasa el numero sino si es null le va a pasar 0.
            itemCategoria = await _repoCategoria.GetAsync(CT.RutaCategoriasApi, id.GetValueOrDefault());

            //Verifico si itemCategoria es nulo después de intentar obtenerlo del repositorio
            if (itemCategoria == null)
            {
                return NotFound();
            }
            return View(itemCategoria);
        }

        //EDIT
            [HttpPost]
        //para protegerse de ataques CSRF (Cross-Site Request Forgery).
        [ValidateAntiForgeryToken]
            public async Task<IActionResult> Update(Categoria categoria) //objeto categoria llamado categoria que viene del create.cshtml
            {
                if (ModelState.IsValid)
                {
                    await _repoCategoria.ActualizarAsync(CT.RutaCategoriasApi + categoria.Id ,categoria);//le manda a la api la url y el objeto categoria del formulario
                    return RedirectToAction(nameof(Index));
                }

                return View(categoria);
            }

        //BORRAR

        [HttpDelete]
        //para protegerse de ataques scsc
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) 
        {
            var status = await _repoCategoria.BorrarAsync(CT.RutaCategoriasApi,id);

            //si hay status si se pudo borrar 
            if (status)
            {
                return Json(new { success = true, message = "Borrado correctamente" });
            }

            return Json(new { success = true, message = "No se pudo borrar" });
        }

    }
}
