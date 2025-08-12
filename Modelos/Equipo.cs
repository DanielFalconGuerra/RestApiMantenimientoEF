using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Equipo
{
    public int IdEquipo { get; set; }

    public string? NombreEquipo { get; set; }

    public virtual ICollection<Problemaequipo> Problemaequipos { get; set; } = new List<Problemaequipo>();
}
