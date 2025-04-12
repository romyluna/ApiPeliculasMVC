namespace PeliculasWeb.Utilidades
{
    public class CT
    {
        public static string UrlBaseApi = "https://localhost:7004/"; //api swagger
        public static string RutaCategoriasApi = UrlBaseApi + "api/v1/Categorias/";
        public static string RutaPeliculasApi = UrlBaseApi + "api/v1/Peliculas/";
        public static string RutaUsuariosApi = UrlBaseApi + "api/v1/Usuarios/";

        //faltan otras rutas para buscar y filtrar pelis por categoria
    }
}
