using Newtonsoft.Json;
using PeliculasWeb.Models;
using PeliculasWeb.Repositorio.IRepositorio;
using System.Text;

namespace PeliculasWeb.Repositorio
{

    public class AccountRepositorio : Repositorio<UsuarioAuth>, IAccountRepositorio //VA A USAR ESTAS 2 INTERFACES
    {

        //INYECCION DE DEPENDCIAS SE DEBE IMPORTAR EL IHttpClientFactory

        private readonly IHttpClientFactory _ClientFactory;

        public AccountRepositorio(IHttpClientFactory clientFactory) :base(clientFactory) 
        {
            _ClientFactory = clientFactory;
        }


        //metodo para hacer login  en el repositorio
        public async Task<UsuarioAuth> LoginAsync(string url, UsuarioAuth itemCrear)
        {
            var Request = new HttpRequestMessage(HttpMethod.Post, url);//creo una solicitud HTTP que le voy a enviar a la api
            if (itemCrear != null)//reviso que los datos que vamos a enviar no sean nulos
            {

                //convertir el objeto itemCrear a un contenido JSON para incluirlo en el cuerpo de una solicitud HTTP
                Request.Content = new StringContent(
                    JsonConvert.SerializeObject(itemCrear),Encoding.UTF8,"application/json");
            }
            else
            {
                return new UsuarioAuth();
            }

            //envia la solicitud a la api

            var cliente = _ClientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(Request);

            //validar si se pudo hacer la validacion correcta y retorna un boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await respuesta.Content.ReadAsStringAsync();
                var usuarioAuthRespuesta = JsonConvert.DeserializeObject<UsuarioAuthRespuesta>(jsonString);

                //mapea los datos de UsuarioAuthRespuesta a UsuarioAuth
                var UsuarioAuth = new UsuarioAuth
                {
                    Id = usuarioAuthRespuesta.Result.Usuario.Id,
                    NombreUsuario = usuarioAuthRespuesta.Result.Usuario.UserName,
                    Nombre = usuarioAuthRespuesta.Result.Usuario.Nombre,
                    Token = usuarioAuthRespuesta.Result.Token

                };

                //solo para comprobar si obtiene el token

                Console.WriteLine($"token recibido { UsuarioAuth.Token} ");
                return UsuarioAuth;
            }
            else
            {
                var errorContent = await respuesta.Content.ReadAsStringAsync();
                Console.WriteLine($"error {respuesta.StatusCode} - {errorContent} ");
                return new UsuarioAuth();
            }

        }

        public async Task<bool> RegisterAsync(string url, UsuarioAuth itemCrear)
        {
            var Request = new HttpRequestMessage(HttpMethod.Post, url);//creo una solicitud HTTP que le voy a enviar a la api
            if (itemCrear != null)//reviso que los datos que vamos a enviar no sean nulos
            {

                //convertir el objeto itemCrear a un contenido JSON para incluirlo en el cuerpo de una solicitud HTTP
                Request.Content = new StringContent(
                    JsonConvert.SerializeObject(itemCrear), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            //envia la solicitud a la api

            var cliente = _ClientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(Request);

            //validar si se pudo hacer la validacion correcta y retorna un boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
