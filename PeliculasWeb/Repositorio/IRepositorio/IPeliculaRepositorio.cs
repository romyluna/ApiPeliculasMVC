using PeliculasWeb.Models;

namespace PeliculasWeb.Repositorio.IRepositorio
{
    public interface IPeliculaRepositorio:IRepositorio<Pelicula> //esta interfaz hereda de IRepositorio
    {
        Task<IEnumerable<Pelicula>> GetPeliculasTodoAsync(String url);
    }
}
