using PeliculasWeb.Models;
using PeliculasWeb.Repositorio.IRepositorio;

namespace PeliculasWeb.Repositorio
{
    public class CategoriaRepositorio : Repositorio<Categoria> , ICategoriaRepositorio
    {
        //inyeccion de dependencias se debe importar el httpClientFactory
        private readonly IHttpClientFactory _clientFactory;

        public CategoriaRepositorio(IHttpClientFactory clientFactory): base (clientFactory) 
        {
            _clientFactory = clientFactory;

        }
    }
}
