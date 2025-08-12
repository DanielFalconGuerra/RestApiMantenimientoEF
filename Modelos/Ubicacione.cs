using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Ubicacione
{
    public int IdUbicacion { get; set; }

    public string? NombreUbicacion { get; set; }

    public int? IdAreaR { get; set; }

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public virtual Areasreporte? IdAreaRNavigation { get; set; }

    public virtual ICollection<Ubicacionsolicitud> Ubicacionsolicituds { get; set; } = new List<Ubicacionsolicitud>();
}
