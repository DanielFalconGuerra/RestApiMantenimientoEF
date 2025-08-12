using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Problemaequipo
{
    public int IdProblemaequipo { get; set; }

    public int? IdProblema { get; set; }

    public int? IdEquipos { get; set; }

    public int? Severidad { get; set; }

    public int? Detectabilidad { get; set; }

    public virtual ICollection<Detalleocurrencium> Detalleocurrencia { get; set; } = new List<Detalleocurrencium>();

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public virtual Equipo? IdEquiposNavigation { get; set; }

    public virtual Problema? IdProblemaNavigation { get; set; }
}
