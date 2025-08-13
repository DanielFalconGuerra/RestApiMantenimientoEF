namespace RestApiMantenimientoEF.Modelos.DTOs
{
    public class AccionDTO
    {
        public int IdEvento { get; set; }
        public int IdColaborador { get; set; }// Por ejemplo: "Inicio Mantenimiento", "Fin Mantenimiento"
        public int IdEstado { get; set; }
        public string AccionTipo { get; set; }
    }
}