using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Color
{
    public int IdColor { get; set; }

    public string? Dia { get; set; }

    public string? Color1 { get; set; }

    public virtual ICollection<Despacha> Despachas { get; set; } = new List<Despacha>();
}
