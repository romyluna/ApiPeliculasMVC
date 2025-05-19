using Microsoft.AspNetCore.Mvc;
using PeliculasWeb.Models;
using PeliculasWeb.Repositorio.IRepositorio;
using PeliculasWeb.Utilidades;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using PeliculasWeb.Models.ViewModels;
using Newtonsoft.Json;


namespace PeliculasWeb.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //instanciamos para poder acceder a todos los metodos

        private readonly IAccountRepositorio _accRepo; //lo llamo _accRepo
        private readonly ICategoriaRepositorio _repoCategoria;
        private readonly IPeliculaRepositorio _repoPelicula; //lo llamo _repoPelicula

        public HomeController(IAccountRepositorio accRepo, ICategoriaRepositorio repoCategoria, IPeliculaRepositorio repoPelicula/*ILogger<HomeController> logger*/)
        {
            //_logger = logger;
            _accRepo = accRepo;
            _repoCategoria = repoCategoria;
            _repoPelicula = repoPelicula;
        }

        //Version 1 sin paginacion seria este codigo:
        //para la visualizacion del index(HOME) : trayendo peliculas y categorias en la vista de cualquier usuario.
        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    IndexVM listaPeliculasCategorias = new IndexVM()
        //    {
        //        ListaCategorias = (IEnumerable<Categoria>) await _repoCategoria.GetTodoAsync(CT.RutaCategoriasApi),
        //        ListaPeliculas = (IEnumerable<Pelicula>)await _repoPelicula.GetPeliculasTodoAsync(CT.RutaPeliculasApi),
        //    };
        //    return View(listaPeliculasCategorias);
        //}

        //con paginacion:
        [HttpGet]
        public async Task<IActionResult> Index(int page = 2)
        {
            const int pageSize = 5; // O el tamaño de página que prefieras
            var url = $"{CT.RutaPeliculasApi}?pageNumber={page}&pageSize={pageSize}";

            var peliculaResponse = await _repoPelicula.GetPeliculasTodoAsync(url);

            Console.WriteLine(JsonConvert.SerializeObject(peliculaResponse));

            IndexVM listaPeliculasCategorias = new IndexVM()
            {
                ListaCategorias = (IEnumerable<Categoria>)await _repoCategoria.GetTodoAsync(CT.RutaCategoriasApi),
                ListaPeliculas = peliculaResponse.Items,
                TotalPages = peliculaResponse.TotalPages,
                CurrentPage = page,
            };
            //Console.WriteLine($"TotalPages: {peliculaResponse.TotalPages}, ItemsCount: {peliculaResponse.Items?.Count()}");


            return View(listaPeliculasCategorias);
        }


        //INDEX-CATEGORIA:con el desplegable de categorias cuando el usuario hace click se muestran las peliculas filtradas si toca terror muestra las pelis de terror y asi.

        [HttpGet]
        public async Task<IActionResult> IndexCategoria(int id)
        {
            var pelisEnCategoria = await _repoPelicula.GetPeliculasEnCategoriaAsync(CT.RutaPeliculasEnCategoriaApi, id);
            return View(pelisEnCategoria);
        }


        //INDEX-BUSQUEDA:con el desplegable de categorias cuando el usuario hace click se muestran las peliculas filtradas si toca terror muestra las pelis de terror y asi.
        [HttpPost]
        public async Task<IActionResult> IndexBusqueda(string nombre)
        {
            var pelisEncontradas = await _repoPelicula.Buscar(CT.RutaPeliculasBusquedaApi, nombre);
            return View(pelisEncontradas);
        }

        /*LOGIN*/

        [HttpGet]
        public IActionResult Login() //Agrego vista de razor vacia a login.-- en la carpeta home crea la nueva vista -- login.
        {
            UsuarioAuth usuario = new UsuarioAuth();
            return View(usuario);
        }

        /*LOGIN FUNCIONALIDAD PARA QUE ANDE*/
        [HttpPost]
        public async Task <IActionResult> Login(UsuarioAuth obj)
        {
            if (ModelState.IsValid)
            {
                UsuarioAuth objUser = await _accRepo.LoginAsync(CT.RutaUsuariosApi + "Login", obj); //ENVIAMOS EL USUARIO
                if (objUser.Token == null)
                {
                    TempData["alert"] = "los datos son incorrectos";
                    return View();   
                }
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Email, objUser.NombreUsuario));

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);

                HttpContext.Session.SetString("JWToken", objUser.Token);
                HttpContext.Session.SetString("Usuario", objUser.NombreUsuario);

                return RedirectToAction("Index");
            }
            else 
            {
                return View();
            }
        }

        /*REGISTRO A NUESTRA PAGINA*/

        [HttpGet]
        public IActionResult Registro() //Agrego vista de razor vacia a Registro.-- en la carpeta home crea la nueva vista -- REGISTRO.
        {
            return View();
        }

        /*funcionalidad de CIERRE DE LOGOUT DE LA SESION*/

        [HttpPost]
        [ValidateAntiForgeryToken] //para proteccion de ataques xss
        public async Task<IActionResult> Registro(UsuarioAuth obj) 
        {
            bool result = await _accRepo.RegisterAsync(CT.RutaUsuariosApi + "Registro", obj);
            if(result == false)
            {
                return View();
            }
            TempData["alert"] = "Registro Correcto";
            return RedirectToAction("Login");
            
        }

        [HttpGet]
        public async Task <IActionResult> Logout()
        {
            //cierra la sesion de autenticacion
            await HttpContext.SignOutAsync();

            //limpiar la sesion del usuario
            HttpContext.Session.Clear();

            //eliminar la cookie de session manualmente

            if(Request.Cookies.ContainsKey(".AspNetCore.Session"))
            {
                Response.Cookies.Delete(".AspNetCore.Session");
            }

            return RedirectToAction("Index");
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
