using PeliculasWeb.Models;

namespace PeliculasWeb.Repositorio.IRepositorio
{
    public interface IPeliculaRepositorio:IRepositorio<Pelicula> //esta interfaz hereda de IRepositorio
    {
        //sin paginacion: Task<IEnumerable<Pelicula>> GetPeliculasTodoAsync(String url);

        //con paginacion:
        Task<PeliculaResponse> GetPeliculasTodoAsync(String url);
    }
}
