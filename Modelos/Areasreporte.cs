using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Areasreporte
{
    public int IdAreaR { get; set; }

    public string? NombreArea { get; set; }

    public virtual ICollection<Problema> Problemas { get; set; } = new List<Problema>();

    public virtual ICollection<Ubicacione> Ubicaciones { get; set; } = new List<Ubicacione>();
}
