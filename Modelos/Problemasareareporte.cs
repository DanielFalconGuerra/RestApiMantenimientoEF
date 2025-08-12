using System;
using System.Collections.Generic;

namespace RestApiMantenimientoEF.Modelos;

public partial class Problemasareareporte
{
    public int IdProblemasareareporte { get; set; }

    public int? IdProblema { get; set; }

    public int? IdAreaR { get; set; }
}
