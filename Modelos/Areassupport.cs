using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Areassupport
{
    public int IdAreaS { get; set; }

    public string? NombreAreaS { get; set; }

    public virtual ICollection<Problema> Problemas { get; set; } = new List<Problema>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
