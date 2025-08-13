using RestApiMantenimientoEF.Interfaces;
using RestApiMantenimientoEF.Modelos;
using Microsoft.EntityFrameworkCore;
using RestApiMantenimientoEF.Modelos.DTOs;

namespace RestApiMantenimientoEF.Repositories
{
    public class EventosRepository : IEventoRepository
    {
        private readonly MantenimientoContext _context;

        public EventosRepository(MantenimientoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Evento>> GetEventosAsync()
        {
            return await _context.Eventos.ToListAsync();
        }

        public async Task<EventoDto> GetEventoByIdAsync(int id)
        {
            //return await _context.Eventos.FindAsync(id);
            var evento = await _context.Eventos
                .Include(e => e.IdUbicacionNavigation)
                .Include(e => e.IdProblemasEquiposNavigation)
                    .ThenInclude(pe => pe.IdProblemaNavigation)
                .Include(e => e.IdProblemasEquiposNavigation)
                    .ThenInclude(pe => pe.IdEquiposNavigation)
                .Include(e => e.Detalleestados.OrderByDescending(de => de.Fecha))
                    .ThenInclude(de => de.IdEstadoNavigation)
                .FirstOrDefaultAsync(e => e.IdEvento == id);

            if (evento == null)
            {
                return null;
            }

            // Encuentra el último detalle del estado
            var ultimoDetalleEstado = evento.Detalleestados.FirstOrDefault();

            var eventoDto = new EventoDto
            {
                IdEvento = evento.IdEvento,
                Observacion = evento.Observacion,
                FechaReporte = evento.FechaReporte,
                FechaResolucion = evento.FechaResolucion,
                Activo = evento.Activo,
                ComentariosFinales = evento.ComentariosFinales,
                NombreUbicacion = evento.IdUbicacionNavigation?.NombreUbicacion,

                // Ahora accedemos a los datos de segundo nivel
                NombreProblema = evento.IdProblemasEquiposNavigation?.IdProblemaNavigation?.NombreProblema,
                NombreEquipo = evento.IdProblemasEquiposNavigation?.IdEquiposNavigation?.NombreEquipo,

                UltimoEstado = ultimoDetalleEstado?.IdEstadoNavigation?.NombreEstado,
                FechaUltimoEstado = ultimoDetalleEstado?.Fecha
            };

            return eventoDto;
        }

        public async Task<IEnumerable<EventoDto>> GetEventosActivosAsync(int idAreaS)
        {
            var eventos = await _context.Eventos
                // 1. Carga explícitamente los datos relacionados.
                .Include(e => e.IdProblemasEquiposNavigation)
                    .ThenInclude(pe => pe.IdProblemaNavigation)
                // 2. Filtra la colección principal.
                .Where(e => e.Activo == true && e.IdProblemasEquiposNavigation != null && e.IdProblemasEquiposNavigation.IdProblemaNavigation != null && e.IdProblemasEquiposNavigation.IdProblemaNavigation.IdAreaS == idAreaS)
                // 3. Sigue cargando otros datos necesarios para el DTO.
                .Include(e => e.IdUbicacionNavigation)
                .Include(e => e.IdProblemasEquiposNavigation)
                    .ThenInclude(pe => pe.IdEquiposNavigation)
                .ToListAsync();

            // Mapeamos los eventos a DTOs
            var eventoDtos = eventos.Select(e => new EventoDto
            {
                IdEvento = e.IdEvento,
                Observacion = e.Observacion,
                FechaReporte = e.FechaReporte,
                FechaResolucion = e.FechaResolucion,
                Activo = e.Activo,
                ComentariosFinales = e.ComentariosFinales,
                NombreUbicacion = e.IdUbicacionNavigation?.NombreUbicacion,
                NombreProblema = e.IdProblemasEquiposNavigation?.IdProblemaNavigation?.NombreProblema,
                NombreEquipo = e.IdProblemasEquiposNavigation?.IdEquiposNavigation?.NombreEquipo
            });

            return eventoDtos;
        }

        public async Task<Evento> CreateEventoAsync(InsertEventoDto evento)
        {
            var nuevoEvento = new Evento
            {
                Observacion = evento.Observacion,
                FechaReporte = DateTime.Now,
                IdUbicacion = evento.IdUbicacion,
                IdProblemasEquipos = evento.IdProblemasEquipos
            };

            _context.Eventos.Add(nuevoEvento);
            await _context.SaveChangesAsync();
            return nuevoEvento;
        }

        public async Task<Evento> CreateEventoConDetallesAsync(InsertEventoDto evento)
        {
            var nuevoEvento = new Evento
            {
                Observacion = evento.Observacion,
                FechaReporte = DateTime.Now,
                IdUbicacion = evento.IdUbicacion,
                IdProblemasEquipos = evento.IdProblemasEquipos
            };

            //_context.Eventos.Add(nuevoEvento);

            var detalleColaborador = new Detallecolaboradore
            {
                IdColaborador = evento.IdColaborador,
                Accion = "Reporte",
                IdAsignador = null
            };

            nuevoEvento.Detallecolaboradores.Add(detalleColaborador);

            //_context.Detallecolaboradores.Add(detalleColaborador);

            var detalleEstado = new Detalleestado
            {
                IdEstado = 1, // 1 = Reportado
                Fecha = DateTime.Now
            };

            nuevoEvento.Detalleestados.Add(detalleEstado);
            //_context.Detalleestados.Add(detalleEstado);

            _context.Eventos.Add(nuevoEvento);

            await _context.SaveChangesAsync();

            return nuevoEvento;
        }

        public async Task<bool> InsertDetallecolaboradoresAsync(Detallecolaboradore detalle)
        {
            _context.Detallecolaboradores.Add(detalle);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> InsertDetalleestadoAsync(Detalleestado detalle)
        {
            _context.Detalleestados.Add(detalle);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateEventoAsync(Evento evento)
        {
            _context.Entry(evento).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Si ocurre una excepción de concurrencia, significa que el evento no existe.
                return false;
            }
        }

        public async Task<bool> DeleteEventoAsync(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null) return false;

            _context.Eventos.Remove(evento);
            return await _context.SaveChangesAsync() > 0;
        }

        // Accion
        public async Task<bool> InsertAccionAsync(AccionDTO accion)
        {
            // Carga el evento principal con todos sus detalles para asegurarnos de que existe
            var evento = await _context.Eventos
                .Include(e => e.Detalleestados)
                .Include(e => e.Detallecolaboradores)
                .FirstOrDefaultAsync(e => e.IdEvento == accion.IdEvento);

            if (evento == null) return false;

            var nuevaDetalleColaboradores = new Detallecolaboradore
            {
                IdColaborador = accion.IdColaborador,
                Accion = accion.AccionTipo
            };

            evento.Detallecolaboradores.Add(nuevaDetalleColaboradores);

            var nuevoDetalleEstado = new Detalleestado
            {
                IdEstado = accion.IdEstado,
                Fecha = DateTime.Now
            };
            evento.Detalleestados.Add(nuevoDetalleEstado);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}