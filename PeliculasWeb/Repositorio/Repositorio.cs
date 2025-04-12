using Newtonsoft.Json;
using PeliculasWeb.Repositorio.IRepositorio;
using System.Collections;
using System.Net.Http;
using System.Text;

namespace PeliculasWeb.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {

        //INYECCION DE DEPENDCIAS SE DEBE IMPORTAR EL IHttpClientFactory

        private readonly IHttpClientFactory _ClientFactory;
        private IHttpClientBuilder clientFactory;

        //constructor de la clase

        public Repositorio(IHttpClientFactory clientFactory)
        {
            _ClientFactory = clientFactory;
        }

        public Repositorio(IHttpClientBuilder clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        //este metodo actualizar sirve solo para categorias/usuarios (no contiene subida de archivos)
        public async Task <bool> ActualizarAsync(string url, T itemActualizar)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Patch, url); //creo una solicitud HTTP que le voy a enviar a la api
            if (itemActualizar != null) //reviso que los datos que vamos a enviar no sean nulos
            {

                peticion.Content = new StringContent(
              JsonConvert.SerializeObject(itemActualizar), Encoding.UTF8, "application/json"
              );

            }
            else
            {
                return false; //si esta vacio el itemactualizar devuelve falso.
            }

            //envia la solicitud a la api

            var cliente = _ClientFactory.CreateClient();
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //validar si se actualizo y retorna un boleano

            if(respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //este metodo actualizar es solo para las peliculas ya que se sube informacion + imagen (Archivo)
        public async Task<bool> ActualizarPeliculaAsync(string url, T peliculaActualizar)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Patch, url); //creo una solicitud HTTP que le voy a enviar a la api

            //se instancia MultipartFormDataContent que permite enviar datos en formato multipart/form-data,
            //se usa para cargar archivos junto con datos de formulario
            // objeto MultipartFormDataContent, que es necesario cuando se envían archivos y datos juntos
            //multipart/form-data es ideal para enviar archivos junto con otros datos de formulario.

            var multipartContent = new MultipartFormDataContent();

            if (peliculaActualizar != null) //reviso que los datos que vamos a enviar no sean nulos
            {
                //serializar cada propiedad de peliculaActualizar y añadirla al contenido
                //multipart-form-data
                foreach (var property in typeof(T).GetProperties()) //recorrer las propiedades del objeto - PROPERTY:todos los campos id,descripcion la imagen etc.
                {
                    var value = property.GetValue(peliculaActualizar);//si la propiedad tiene un valor se agrega a la solicitud
                    if(value != null)
                    {
                        if (property.PropertyType == typeof(IFormFile))//aca detecto cuando es una subida de archivo
                        {
                            //proceso de envio del archivo (no de subida porque eso lo hace la API)
                            var file = value as IFormFile;
                            if (file !=null)
                            {
                                var streamContent = new StreamContent(file.OpenReadStream());
                                streamContent.Headers.ContentType = 
                                    new System.Net.Http.Headers.MediaTypeHeaderValue("File.ContentType");
                               
                                //enviamos todo el contenido como multipartcontent

                                multipartContent.Add(streamContent,property.Name,file.FileName);
                            }
                        }
                        else
                        {
                            //sino hay un archivo para subir  simplemente se convierte en un valor de texto y se agrega a la solicitud
                            var stringContent = new StringContent(value.ToString());
                            multipartContent.Add(stringContent, property.Name);

                        }
                    }
                }
            }
            else
            {
                return false; //si esta vacio el peliculaActualizar devuelve falso.
            }

            //envia la solicitud a la api
            peticion.Content = multipartContent;
            var cliente = _ClientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //validar si se actualizo y retorna un booleano

            if (respuesta.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> BorrarAsync(string url, int id)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Delete, url + id); //creo una solicitud HTTP que le voy a enviar a la api
            
            //envia la solicitud a la api

            var cliente = _ClientFactory.CreateClient();
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //validar si se actualizo y retorna un boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable> Buscar(string url, string nombre)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, url + nombre); //creo una solicitud HTTP que le voy a enviar a la api SUMO EL nombre PARA QUE SE PASE POR LA PETICION 

            //envia la solicitud a la api

            var cliente = _ClientFactory.CreateClient();
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //validar si se actualizo y retorna un boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Para devolver la informacion que busca

                var jsonString = await respuesta.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);//<IEnumerable<T>> se agrega asi porque sino no te deja deserializarlo x ser IEnumerable

            }
            else
            {
                return null;
            }
        }

        //crear una nueva categoria-usuario (NO PELICULA)
        public async Task<bool> CrearAsync(string url, T itemCrear)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Post, url); //creo una solicitud HTTP que le voy a enviar a la api
            if (itemCrear != null) //reviso que los datos que vamos a enviar no sean nulos
            {
                peticion.Content = new StringContent(JsonConvert.SerializeObject(itemCrear),
                Encoding.UTF8, "application/json"); // Cambiado a "application/json"
            }
            else
            {
                return false; //si esta vacio el itemactualizar devuelve falso.
            }

            //envia la solicitud a la api

            var cliente = _ClientFactory.CreateClient();
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //validar si se actualizo y retorna un boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CrearPeliculaAsync(string url, T peliculaCrear)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Post, url); //creo una solicitud HTTP que le voy a enviar a la api

            //se instancia MultipartFormDataContent que permite enviar datos en formato multipart/form-data,
            //se usa para cargar archivos junto con datos de formulario
            // objeto MultipartFormDataContent, que es necesario cuando se envían archivos y datos juntos
            //multipart/form-data es ideal para enviar archivos junto con otros datos de formulario.

            var multipartContent = new MultipartFormDataContent();

            if (peliculaCrear != null) //reviso que los datos que vamos a enviar no sean nulos
            {
                //serializar cada propiedad de peliculaActualizar y añadirla al contenido
                //multipart-form-data
                foreach (var property in typeof(T).GetProperties()) //recorrer las propiedades del objeto - PROPERTY:todos los campos id,descripcion la imagen etc.
                {
                    var value = property.GetValue(peliculaCrear);//si la propiedad tiene un valor se agrega a la solicitud
                    if (value != null)
                    {
                        if (property.PropertyType == typeof(IFormFile))//aca detecto cuando es una subida de archivo
                        {
                            //proceso de envio del archivo (no de subida porque eso lo hace la API)
                            var file = value as IFormFile;
                            if (file != null)
                            {
                                var streamContent = new StreamContent(file.OpenReadStream());
                                streamContent.Headers.ContentType =
                                    new System.Net.Http.Headers.MediaTypeHeaderValue("File.ContentType");

                                //enviamos todo el contenido como multipartcontent

                                multipartContent.Add(streamContent, property.Name, file.FileName);
                            }
                        }
                        else
                        {
                            //sino hay un archivo para subir  simplemente se convierte en un valor de texto y se agrega a la solicitud
                            var stringContent = new StringContent(value.ToString());
                            multipartContent.Add(stringContent, property.Name);

                        }
                    }
                }
            }
            else
            {
                return false; //si esta vacio el peliculaActualizar devuelve falso.
            }

            //envia la solicitud a la api
            peticion.Content = multipartContent;
            var cliente = _ClientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //validar si se actualizo y retorna un booleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //ger por id
        public async Task<T> GetAsync(string url, int id)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, url + id); //creo una solicitud HTTP que le voy a enviar a la api SUMO EL ID PARA QUE SE PASE POR LA PETICION 

            //envia la solicitud a la api

            var cliente = _ClientFactory.CreateClient();
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //validar si se actualizo y retorna un boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Para devolver la informacion que busca

                var jsonString = await respuesta.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(jsonString);
                    
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable> GetPeliculasEnCategoriaAsync(string url, int categoriaId)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, url + categoriaId); //creo una solicitud HTTP que le voy a enviar a la api SUMO EL categoriaId PARA QUE SE PASE POR LA PETICION 

            //envia la solicitud a la api

            var cliente = _ClientFactory.CreateClient();
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //validar si se actualizo y retorna un boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Para devolver la informacion que busca

                var jsonString = await respuesta.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);//<IEnumerable<T>> se agrega asi porque sino no te deja deserializarlo x ser IEnumerable

            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable> GetTodoAsync(string url)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, url); //creo una solicitud HTTP que le voy a enviar a la api SUMO EL ID PARA QUE SE PASE POR LA PETICION 

            //envia la solicitud a la api

            var cliente = _ClientFactory.CreateClient();
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //validar si se actualizo y retorna un boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Para devolver la informacion que busca

                var jsonString = await respuesta.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);//<IEnumerable<T>> se agrega asi porque sino no te deja deserializarlo x ser IEnumerable

            }
            else
            {
                return null;
            }
        }
    }
}
