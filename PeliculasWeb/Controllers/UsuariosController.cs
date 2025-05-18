using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeliculasWeb.Models;
using PeliculasWeb.Repositorio.IRepositorio;
using PeliculasWeb.Utilidades;

namespace PeliculasWeb.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {


        //instanciamos para poder acceder a todos los metodos

        private readonly IUsuarioRepositorio _repoUsuario; //lo llamo _repoCategoria

        public UsuariosController(IUsuarioRepositorio repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }



        [HttpGet]
        public IActionResult Index()
        {
            return View(new Usuario(){});
        }


        [HttpGet]

        //aca con este metodo llamamos al metodo de la api para que la api nos traiga la informacion que necesitamos para el front-end
        public async Task<IActionResult> GetTodosUsuarios()
        {
            return Json(new { data = await _repoUsuario.GetTodoAsync(CT.RutaUsuariosApi) });
        }

    }
}
