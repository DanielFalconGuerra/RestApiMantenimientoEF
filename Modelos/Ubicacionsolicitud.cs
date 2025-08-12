using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Ubicacionsolicitud
{
    public int IdUbicacionSolicitud { get; set; }

    public string? Celula { get; set; }

    public bool? Activo { get; set; }

    public int? IdUbicacion { get; set; }

    public virtual Ubicacione? IdUbicacionNavigation { get; set; }

    public virtual ICollection<Solicitudtrabajo> Solicitudtrabajos { get; set; } = new List<Solicitudtrabajo>();
}
