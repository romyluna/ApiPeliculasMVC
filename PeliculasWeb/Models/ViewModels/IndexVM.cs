namespace PeliculasWeb.Models.ViewModels
{
    public class IndexVM
    {
        public IEnumerable<Categoria> ListaCategorias { get; set; }
        public IEnumerable<Pelicula> ListaPeliculas { get; set; }

        //PARA TRAER EL TOTAL DE PAGINAS EN LA VISUAL DEL INDEX:

        public int TotalPages { get; set; }//TOTAL DE PAGINAS DISPONIBLES

        public int CurrentPage { get; set; }//pagina actal

    }
}
