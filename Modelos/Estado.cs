using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Estado
{
    public int IdEstado { get; set; }

    public string? NombreEstado { get; set; }

    public virtual ICollection<Detalleestado> Detalleestados { get; set; } = new List<Detalleestado>();
}
