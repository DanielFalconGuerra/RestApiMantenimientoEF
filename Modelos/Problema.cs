using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Problema
{
    public int IdProblema { get; set; }

    public int? IdAreaS { get; set; }

    public string? NombreProblema { get; set; }

    public string? Descripcion { get; set; }

    public int? IdAreaR { get; set; }

    public virtual Areasreporte? IdAreaRNavigation { get; set; }

    public virtual Areassupport? IdAreaSNavigation { get; set; }

    public virtual ICollection<Problemaequipo> Problemaequipos { get; set; } = new List<Problemaequipo>();

    public virtual ICollection<Solicitudtrabajo> Solicitudtrabajos { get; set; } = new List<Solicitudtrabajo>();
}
