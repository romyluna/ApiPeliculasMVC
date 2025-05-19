using Newtonsoft.Json;
using PeliculasWeb.Models;
using PeliculasWeb.Repositorio.IRepositorio;
using System.Net.Http;

namespace PeliculasWeb.Repositorio
{

    public class PeliculaRepositorio : Repositorio<Pelicula>, IPeliculaRepositorio
    {

        //inyeccion de dependencias se debe importar el httpClientFactory

        private readonly IHttpClientFactory _clientFactory;

        public PeliculaRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        //copie y pegue todo el metodo desde repositorio getTodoAsync y le cambie el nombre 
        //En vez de usar el generico ponemos un metodo particular para peliculas que es este para soportar paginacion 
        /*SIN PAGINACION USARIA ESTE CODIGO:*/

        //public async Task<IEnumerable<Pelicula>> GetPeliculasTodoAsync(string url)
        //{
        //    var peticion = new HttpRequestMessage(HttpMethod.Get, url); //creo una solicitud HTTP que le voy a enviar a la api SUMO EL ID PARA QUE SE PASE POR LA PETICION 

        //    //envia la solicitud a la api

        //    var cliente = _clientFactory.CreateClient();
        //    HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

        //    //validar si se actualizo y retorna un boleano

        //    if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        //Para devolver la informacion que busca

        //        var jsonString = await respuesta.Content.ReadAsStringAsync();//que lea el contenido

        //        //deserealizar a PeliculaResponse
        //        var peliculaResponse = JsonConvert.DeserializeObject<PeliculaResponse>(jsonString);

        //        //devolver la lista de peliculas
        //        return peliculaResponse?.Items ?? new List<Pelicula>();

        //    }
        //    else
        //    {
        //        return new List<Pelicula>();
        //    }
        //}

        /*CON PAGINACION*/
        public async Task<PeliculaResponse> GetPeliculasTodoAsync(string url)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, url); //creo una solicitud HTTP que le voy a enviar a la api SUMO EL ID PARA QUE SE PASE POR LA PETICION 

            //envia la solicitud a la api

            var cliente = _clientFactory.CreateClient();
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //validar si se actualizo y retorna un boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Para devolver la informacion que busca

                var jsonString = await respuesta.Content.ReadAsStringAsync();//que lea el contenido

                //deserealizar a PeliculaResponse
                var peliculaResponse = JsonConvert.DeserializeObject<PeliculaResponse>(jsonString);

                //devolver la lista de peliculas
                return peliculaResponse?? new PeliculaResponse();

            }
            else
            {
                return new PeliculaResponse();
            }
        }






    }
   
}


 