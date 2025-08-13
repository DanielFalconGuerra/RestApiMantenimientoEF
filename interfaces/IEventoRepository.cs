using RestApiMantenimientoEF.Modelos;
using RestApiMantenimientoEF.Modelos.DTOs;

namespace RestApiMantenimientoEF.Interfaces
{
    public interface IEventoRepository
    {
        Task<IEnumerable<Evento>> GetEventosAsync();
        Task<EventoDto> GetEventoByIdAsync(int id);
        Task<IEnumerable<EventoDto>> GetEventosActivosAsync(int idAreaS);
        Task<bool> InsertDetallecolaboradoresAsync(Detallecolaboradore detalle);
        Task<bool> InsertDetalleestadoAsync(Detalleestado detalle);
        Task<Evento> CreateEventoConDetallesAsync(InsertEventoDto evento);
        Task<Evento> CreateEventoAsync(InsertEventoDto evento);
        Task<bool> UpdateEventoAsync(Evento evento);
        Task<bool> DeleteEventoAsync(int id);

        // Accion
        Task<bool> InsertAccionAsync(AccionDTO accion);
    }
}