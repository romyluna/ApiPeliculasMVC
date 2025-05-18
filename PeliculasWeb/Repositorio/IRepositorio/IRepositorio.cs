using System.Collections;
using System.Security;

namespace PeliculasWeb.Repositorio.IRepositorio
{
    
    //<t> : plantilla generica que se va adaptar a cualquiera de las entidades(pelicula etc)

    public interface IRepositorio<T> where T : class
    {
        //IEnumerable: devuelve una lista de ya sea categorias/usuarios/peliculas
        Task <IEnumerable> GetTodoAsync(string url);

        //IEnumerable: devuelve las peliculas en una categoria
        Task<IEnumerable> GetPeliculasEnCategoriaAsync(string url, int categoriaId);

        //para buscar pelicula puede pasar que una pelicula tenga la 1,2,3 con el mismo nombre (x eso es una lista)
        Task<IEnumerable> Buscar(string url, string nombre);
        
        //para traer una entidad de una manera individual ejemplo: buscar una pelicula por id 2 , o categoria 2 etc.
        Task<T>GetAsync(string url,int id);

        //metodo que no hace uso de subida de archivos (como peliculas por eso lo aperturo despues en el siguiente a pelicula
        //aca seria para categorias/usuarios
        Task<bool> CrearAsync(string url, T itemCrear ,string token);//creo una variable que se llame token en lo que no quiero que vea cualquier persona sino que este autenticada 
        Task<bool> CrearPeliculaAsync(string url, T peliculaCrear, string token);//creo una variable que se llame token en lo que no quiero que vea cualquier persona sino que este autenticada 

        //para actualizar usuarios/categorias
        Task<bool> ActualizarAsync(string url, T itemActualizar, string token);//creo una variable que se llame token en lo que no quiero que vea cualquier persona sino que este autenticada 

        //para actualizar peliculas (aparte por lo de la subida de archivos q el anterior no tiene)
        Task<bool> ActualizarPeliculaAsync(string url, T peliculaActualizar, string token);//creo una variable que se llame token en lo que no quiero que vea cualquier persona sino que este autenticada 
        Task<bool> BorrarAsync(string url, int id, string token);//creo una variable que se llame token en lo que no quiero que vea cualquier persona sino que este autenticada 
    }
}
