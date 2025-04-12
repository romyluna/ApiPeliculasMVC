namespace PeliculasWeb.Models
{
    public class PeliculaResponse
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalItem { get; set; }

        public List<Pelicula>Items { get; set; }

    }
}
