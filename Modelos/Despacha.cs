using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Despacha
{
    public int IdDespacha { get; set; }

    public int? IdCarro { get; set; }

    public int? IdSolicitudTrabajo { get; set; }

    public int? IdColor { get; set; }

    public DateTime? FechaDespacho { get; set; }

    public virtual Carro? IdCarroNavigation { get; set; }

    public virtual Color? IdColorNavigation { get; set; }

    public virtual Solicitudtrabajo? IdSolicitudTrabajoNavigation { get; set; }
}
