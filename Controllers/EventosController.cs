using Microsoft.AspNetCore.Mvc;
using RestApiMantenimientoEF.Modelos;
using Microsoft.EntityFrameworkCore;  // Necesario para ToListAsync
using RestApiMantenimientoEF.Modelos.DTOs;
using RestApiMantenimientoEF.Interfaces;
using RestApiMantenimientoEF.Modelos.Responses;
using Microsoft.AspNetCore.Authorization;

namespace RestApiMantenimientoEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly MantenimientoContext _context;
        private readonly IEventoRepository _eventoRepository;

        public EventosController(MantenimientoContext context, IEventoRepository eventoRepository)
        {
            _context = context;
            _eventoRepository = eventoRepository;
        }

        // Aquí puedes agregar métodos específicos para manejar eventos si es necesario
        [Authorize]
        [HttpGet("get")]
        public async Task<ActionResult<AccionResponse<IEnumerable<Evento>>>> GetEventos()
        {
            /*if (_context.Eventos == null)
            {
                return NotFound();
            }
            return await _context.Eventos.ToListAsync();*/

            var eventos = await _eventoRepository.GetEventosAsync();
            if (eventos == null || !eventos.Any())
            {
                return NotFound();
            }
            //return eventos.ToList();
            return new AccionResponse<IEnumerable<Evento>>
            {
                Status = 200,
                Message = "Eventos obtenidos con éxito.",
                Data = eventos.ToList()
            };
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<AccionResponse<EventoDto>>> GetEventoById(int id)
        {
            //var evento = await _context.Eventos.FindAsync(id);
            /*var evento = await _context.Eventos
                .Include(e => e.IdUbicacionNavigation)
                .Include(e => e.IdProblemasEquiposNavigation)
                    .ThenInclude(pe => pe.IdProblemaNavigation)
                .Include(e => e.IdProblemasEquiposNavigation)
                    .ThenInclude(pe => pe.IdEquiposNavigation)
                .FirstOrDefaultAsync(e => e.IdEvento == id);*/

            var evento = await _eventoRepository.GetEventoByIdAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            return new AccionResponse<EventoDto>
            {
                Status = 200,
                Message = "Evento obtenido con éxito.",
                Data = evento
            };
        }

        // Obtener los eventos activos de un area de soporte
        [HttpGet("get/activos/{idAreaS}")]
        public async Task<ActionResult<AccionResponse<IEnumerable<EventoDto>>>> GetEventosActivos(int idAreaS)
        {
            var eventos = await _eventoRepository.GetEventosActivosAsync(idAreaS);
            if (eventos == null || !eventos.Any())
            {
                return NotFound();
            }
            //return eventos.ToList();
            return new AccionResponse<IEnumerable<EventoDto>>
            {
                Status = 200,
                Message = "Eventos activos obtenidos con éxito.",
                Data = eventos.ToList()
            };
        }

        [HttpPost("create/")]
        public async Task<ActionResult<AccionResponse<EventoDto>>> PostEvento([FromBody] InsertEventoDto evento)
        {
            var eventoCreado = await _eventoRepository.CreateEventoAsync(evento);
            if (eventoCreado == null)
            {
                return Problem("Error al crear el evento.");
            }
            var DetallecolaboradorInsert = await _eventoRepository.InsertDetallecolaboradoresAsync(new Detallecolaboradore
            {
                IdColaborador = evento.IdColaborador,
                IdEvento = eventoCreado.IdEvento
            });
            var DetalleestadoInsert = await _eventoRepository.InsertDetalleestadoAsync(new Detalleestado
            {
                IdEstado = 1, // 1 = Reportado
                Fecha = DateTime.Now,
                IdEvento = eventoCreado.IdEvento
            });

            // Aquí está el cambio. Apuntamos al nombre del método y pasamos el id.
            //return CreatedAtAction(nameof(GetEventoById), new { id = eventoCreado.IdEvento }, eventoCreado);
            return new AccionResponse<EventoDto>
            {
                Status = 201,
                Message = "Evento creado con éxito.",
                Data = new EventoDto
                {
                    IdEvento = eventoCreado.IdEvento,
                    Observacion = eventoCreado.Observacion,
                    FechaReporte = eventoCreado.FechaReporte,
                    FechaResolucion = eventoCreado.FechaResolucion,
                    Activo = eventoCreado.Activo,
                    ComentariosFinales = eventoCreado.ComentariosFinales,
                    NombreUbicacion = eventoCreado.IdUbicacionNavigation?.NombreUbicacion
                }
            };
        }

        [HttpPost("create/detalles")]
        public async Task<ActionResult<AccionResponse<EventoDto>>> PostEventoConDetalles([FromBody] InsertEventoDto evento)
        {
            var eventoCreado = await _eventoRepository.CreateEventoConDetallesAsync(evento);
            if (eventoCreado == null)
            {
                return Problem("Error al crear el evento.");
            }
            return new AccionResponse<EventoDto>
            {
                Status = 201,
                Message = "Evento creado con éxito.",
                Data = new EventoDto
                {
                    IdEvento = eventoCreado.IdEvento,
                    Observacion = eventoCreado.Observacion,
                    FechaReporte = eventoCreado.FechaReporte,
                    FechaResolucion = eventoCreado.FechaResolucion,
                    Activo = eventoCreado.Activo,
                    ComentariosFinales = eventoCreado.ComentariosFinales,
                    NombreUbicacion = eventoCreado.IdUbicacionNavigation?.NombreUbicacion
                }
            };

            //return CreatedAtAction(nameof(GetEventoById), new { id = eventoCreado.IdEvento }, eventoCreado);
        }

        [HttpPut("put/{id}")]
        public async Task<ActionResult<AccionResponse<EventoDto>>> PutEvento(int id, [FromBody] Evento evento)
        {
            if (id != evento.IdEvento)
            {
                return BadRequest(); // Retorna un 400 si el ID de la URL no coincide con el ID del objeto
            }

            var eventoActualizado = await _eventoRepository.UpdateEventoAsync(evento);
            if (!eventoActualizado)
            {
                return Problem("Error al actualizar el evento.");
            }

            return new AccionResponse<EventoDto>
            {
                Status = 204,
                Message = "Evento actualizado con éxito.",
                Data = new EventoDto
                {
                    IdEvento = evento.IdEvento,
                    Observacion = evento.Observacion,
                    FechaReporte = evento.FechaReporte,
                    FechaResolucion = evento.FechaResolucion,
                    Activo = evento.Activo,
                    ComentariosFinales = evento.ComentariosFinales,
                    NombreUbicacion = evento.IdUbicacionNavigation?.NombreUbicacion
                }
            };
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<AccionResponse<bool>>> DeleteEvento(int id)
        {
            var eventoEliminado = await _eventoRepository.DeleteEventoAsync(id);
            if (!eventoEliminado)
            {
                return Problem("Error al eliminar el evento.");
            }
            return new AccionResponse<bool>
            {
                Status = 204,
                Message = "Evento eliminado con éxito.",
                Data = true
            };
        }

        [HttpPost("InsertAccion")]
        public async Task<ActionResult<AccionResponse<bool>>> InsertarAccionAsync([FromBody] AccionDTO accionDTO)
        {
            try
            {
                var insertarNuevaAccion = await _eventoRepository.InsertAccionAsync(accionDTO);
                return new AccionResponse<bool>
                {
                    Status = 201,
                    Message = "Acción insertada con éxito.",
                    Data = insertarNuevaAccion
                };
            }
            catch (Exception ex)
            {
                return new AccionResponse<bool>
                {
                    Status = 500,
                    Message = $"Error al insertar la acción: {ex.Message}",
                    Data = false
                };
            }
        }
        
        
    }
}