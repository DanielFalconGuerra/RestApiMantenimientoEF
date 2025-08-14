namespace RestApiMantenimientoEF.Modelos.DTOs
{
    public class LoginDTO
    {
        public string NoColaborador { get; set; }
        public string key { get; set; }
    }

    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public int IdAreaS { get; set; }
    }
}