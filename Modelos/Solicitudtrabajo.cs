using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Solicitudtrabajo
{
    public int IdSolicitudTrabajo { get; set; }

    public int IdProblema { get; set; }

    public int IdUbicacionSolicitud { get; set; }

    public DateTime? Fecha { get; set; }

    public int? Estatus { get; set; }

    public int? Solicita { get; set; }

    public virtual ICollection<Despacha> Despachas { get; set; } = new List<Despacha>();

    public virtual Problema IdProblemaNavigation { get; set; } = null!;

    public virtual Ubicacionsolicitud IdUbicacionSolicitudNavigation { get; set; } = null!;

    public virtual User? SolicitaNavigation { get; set; }
}
