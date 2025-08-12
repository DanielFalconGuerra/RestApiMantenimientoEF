using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Detalleocurrencium
{
    public int IdOcurrencia { get; set; }

    public int? IdProblemaEquipo { get; set; }

    public DateOnly? Fecha { get; set; }

    public int? Ocurrencia { get; set; }

    public int? FactorDeUrgencia { get; set; }

    public virtual Problemaequipo? IdProblemaEquipoNavigation { get; set; }
}
