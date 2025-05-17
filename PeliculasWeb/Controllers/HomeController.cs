using Microsoft.AspNetCore.Mvc;
using PeliculasWeb.Models;
using PeliculasWeb.Repositorio.IRepositorio;
using PeliculasWeb.Utilidades;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace PeliculasWeb.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //instanciamos para poder acceder a todos los metodos

        private readonly IAccountRepositorio _accRepo; //lo llamo _accRepo

        public HomeController(IAccountRepositorio accRepo/*ILogger<HomeController> logger*/)
        {
            //_logger = logger;
            _accRepo = accRepo;
        }

        public IActionResult Index()
        {
            return View();
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
