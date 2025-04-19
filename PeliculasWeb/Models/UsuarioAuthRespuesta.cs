namespace PeliculasWeb.Models
{
    public class UsuarioAuthRespuesta
    {
        //respuesta desde la api

        public string StatusCode { get; set; }
        public string IsSuccess { get; set; }
        public List<string> ErrorMessage { get; set; }
        public ResultData Result { get; set; } // hay que armar otra clase para resultData

    }
}
