using Microsoft.AspNetCore.Mvc;
using RestApiMantenimientoEF.Modelos;
using Microsoft.EntityFrameworkCore;  // Necesario para ToListAsync
using RestApiMantenimientoEF.Modelos.DTOs;

namespace RestApiMantenimientoEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly MantenimientoContext _context;

        public EventosController(MantenimientoContext context)
        {
            _context = context;
        }

        // Aquí puedes agregar métodos específicos para manejar eventos si es necesario
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventos()
        {
            if (_context.Eventos == null)
            {
                return NotFound();
            }
            return await _context.Eventos.ToListAsync();
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<EventoDto>> GetEvento(int id)
        {
            //var evento = await _context.Eventos.FindAsync(id);
            var evento = await _context.Eventos
                .Include(e => e.IdUbicacionNavigation)
                .Include(e => e.IdProblemasEquiposNavigation)
                    .ThenInclude(pe => pe.IdProblemaNavigation)
                .Include(e => e.IdProblemasEquiposNavigation)
                    .ThenInclude(pe => pe.IdEquiposNavigation)
                .FirstOrDefaultAsync(e => e.IdEvento == id);

            if (evento == null)
            {
                return NotFound();
            }

            // Mapeamos el modelo de EF al DTO
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
                NombreEquipo = evento.IdProblemasEquiposNavigation?.IdEquiposNavigation?.NombreEquipo
            };

            return eventoDto;
        }

        [HttpPost("create/{IdColaborador}")]
        public async Task<ActionResult<Evento>> PostEvento(int IdColaborador, [FromBody] Evento evento)
        {
            if (_context.Eventos == null)
            {
                return Problem("Entity set 'MantenimientoContext.Eventos' is null.");
            }
            _context.Eventos.Add(evento);

            // Creamos y configuramos el registro para Detallecolaboradore.
            var detalleColaborador = new Detallecolaboradore
            {
                IdColaborador = IdColaborador, // Asignamos un colaborador existente
                Accion = "Reporte",
                IdAsignador = null
                // No asignamos el IdEvento todavía. EF lo hará por nosotros.
            };

            // Agregamos el detalle al evento. Esto crea el vínculo.
            evento.Detallecolaboradores.Add(detalleColaborador);

            // Creamos y configuramos el registro para Detalleestado.
            var detalleEstado = new Detalleestado
            {
                IdEstado = 1, // 1 = Reportado
                Fecha = DateTime.Now, // La fecha y hora actual del servidor.
                // No asignamos el IdEvento. EF lo hará por nosotros.
            };    

            // 5. Agregamos el detalle al evento. Esto crea el vínculo.
            evento.Detalleestados.Add(detalleEstado);            

            // Guardamos los cambios en la base de datos en una sola transaccion
            await _context.SaveChangesAsync();

            // Aquí está el cambio. Apuntamos al nombre del método y pasamos el id.
            return CreatedAtAction(nameof(GetEvento), new { id = evento.IdEvento }, evento);
        }

        [HttpPut("put/{id}")]
        public async Task<IActionResult> PutEvento(int id, [FromBody] Evento evento)
        {
            if (id != evento.IdEvento)
            {
                return BadRequest(); // Retorna un 400 si el ID de la URL no coincide con el ID del objeto
            }

            _context.Entry(evento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
                {
                    return NotFound(); // Retorna un 404 si el evento no existe
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Retorna un 204 para indicar que la operación fue exitosa
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            if (_context.Eventos == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventoExists(int id)
        {
            return (_context.Eventos?.Any(e => e.IdEvento == id)).GetValueOrDefault();
        }
    }
}