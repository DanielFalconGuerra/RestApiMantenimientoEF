namespace RestApiMantenimientoEF.Modelos.DTOs
{
    public class EventoDto
    {
        public int IdEvento { get; set; }
        public string? Observacion { get; set; }
        public DateTime? FechaReporte { get; set; }
        public DateTime? FechaResolucion { get; set; }
        public bool? Activo { get; set; }
        public string? ComentariosFinales { get; set; }

        // Propiedades de la ubicación
        public string? NombreUbicacion { get; set; }

        // Propiedades del problema y equipo
        public string? NombreProblema { get; set; }
        public string? NombreEquipo { get; set; }
    }
}