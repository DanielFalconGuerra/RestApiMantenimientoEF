using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Detalleestado
{
    public int IdDetalleEstado { get; set; }

    public int? IdEstado { get; set; }

    public int? IdEvento { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual Evento? IdEventoNavigation { get; set; }
}
