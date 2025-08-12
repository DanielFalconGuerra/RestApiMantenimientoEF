using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Carro
{
    public int IdCarro { get; set; }

    public string Letra { get; set; } = null!;

    public virtual ICollection<Despacha> Despachas { get; set; } = new List<Despacha>();
}
