using Microsoft.AspNetCore.Mvc;
using RestApiMantenimientoEF.Modelos;
using Microsoft.EntityFrameworkCore;  // Necesario para ToListAsync
using RestApiMantenimientoEF.Modelos.DTOs;
using RestApiMantenimientoEF.Interfaces;

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
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventos()
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
            return eventos.ToList();
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<EventoDto>> GetEventoById(int id)
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

            return Ok(evento);
        }

        // Obtener los eventos activos de un area de soporte
        [HttpGet("get/activos/{idAreaS}")]
        public async Task<ActionResult<IEnumerable<EventoDto>>> GetEventosActivos(int idAreaS)
        {
            var eventos = await _eventoRepository.GetEventosActivosAsync(idAreaS);
            if (eventos == null || !eventos.Any())
            {
                return NotFound();
            }
            return eventos.ToList();
        }

        [HttpPost("create/")]
        public async Task<ActionResult<Evento>> PostEvento([FromBody] InsertEventoDto evento)
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
            return CreatedAtAction(nameof(GetEventoById), new { id = eventoCreado.IdEvento }, eventoCreado);
        }

        [HttpPost("create/detalles")]
        public async Task<ActionResult> PostEventoConDetalles([FromBody] InsertEventoDto evento)
        {
            var eventoCreado = await _eventoRepository.CreateEventoConDetallesAsync(evento);
            if (eventoCreado == null)
            {
                return Problem("Error al crear el evento.");
            }

            return CreatedAtAction(nameof(GetEventoById), new { id = eventoCreado.IdEvento }, eventoCreado);
        }

        [HttpPut("put/{id}")]
        public async Task<IActionResult> PutEvento(int id, [FromBody] Evento evento)
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

            return NoContent(); // Retorna un 204 para indicar que la operación fue exitosa
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var eventoEliminado = await _eventoRepository.DeleteEventoAsync(id);
            if (!eventoEliminado)
            {
                return Problem("Error al eliminar el evento.");
            }

            return NoContent(); // Retorna un 204 para indicar que la operación fue exitosa
        }

        [HttpPost("InsertAccion")]
        public async Task<IActionResult> InsertarAccionAsync([FromBody] AccionDTO accionDTO)
        {
            try
            {
                var insertarNuevaAccion = await _eventoRepository.InsertAccionAsync(accionDTO);
                return NoContent(); // Retorna un 204 para indicar que la operación fue exitosa
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 Not Found
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // 400 Bad Request
            }
            catch (Exception ex)
            {
                return Problem("Error al insertar la acción.");
            }
        }
    }
}