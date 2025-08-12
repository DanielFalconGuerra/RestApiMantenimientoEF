using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Detallecolaboradore
{
    public int IdDetalleC { get; set; }

    public int? IdColaborador { get; set; }

    public int? IdEvento { get; set; }

    public string? Accion { get; set; }

    public int? IdAsignador { get; set; }

    public virtual User? IdColaboradorNavigation { get; set; }

    public virtual Evento? IdEventoNavigation { get; set; }
}
