using PeliculasWeb.Models;

namespace PeliculasWeb.Repositorio.IRepositorio
{
    public interface IAccountRepositorio : IRepositorio<UsuarioAuth>
    {

        //metodos 
        Task<UsuarioAuth> LoginAsync(string url, UsuarioAuth itemCrear);

        //si se pudo registrar o no el usuario
        Task<bool> RegisterAsync(string url, UsuarioAuth itemCrear);
    }
}
