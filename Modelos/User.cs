using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class User
{
    public int NoColaborador { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoP { get; set; }

    public string? ApellidoM { get; set; }

    public int? NoSupervisor { get; set; }

    public int? IdAreaS { get; set; }

    public string? Disponible { get; set; }

    public virtual ICollection<Detallecolaboradore> Detallecolaboradores { get; set; } = new List<Detallecolaboradore>();

    public virtual Areassupport? IdAreaSNavigation { get; set; }

    public virtual ICollection<Solicitudtrabajo> Solicitudtrabajos { get; set; } = new List<Solicitudtrabajo>();
}
