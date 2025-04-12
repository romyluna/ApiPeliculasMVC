using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PeliculasWeb.Models
{
    public class Pelicula
    {
        public Pelicula() 
        {
            FechaCreacion = DateTime.Now;
        }
    
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "la descripcion es obligatorio")]
        public string Descripcion { get; set; }
        public string Duracion { get; set; }

        public IFormFile Image { get; set; } //este lo agrego es el que nos va a permitir subir la imagen de la pelicula
        //public string? RutaLocalIMagen { get; set; } la volo
        public string? RutaIMagen { get; set; }

       // [Required(ErrorMessage = "la descripcion es obligatorio")]
        public enum TipoClasificacion { Siete, Trece, Dieciseis, Diechiocho }
        public TipoClasificacion Clasificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }

        //Relación con Categoria
        public int categoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
