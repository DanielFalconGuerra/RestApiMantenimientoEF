using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestApiMantenimientoEF.Modelos;

public partial class Evento
{
    public int IdEvento { get; set; }

    public int? IdUbicacion { get; set; }

    public int? IdProblemasEquipos { get; set; }

    public string? Observacion { get; set; }

    public DateTime? FechaReporte { get; set; }

    public DateTime? FechaResolucion { get; set; }

    public bool? Activo { get; set; }

    public string? ComentariosFinales { get; set; }

    public string? Enlace { get; set; }

    public string? EnlaceMantenimiento { get; set; }

    public int? Disabled { get; set; }

    [JsonIgnore]
    public virtual ICollection<Detallecolaboradore> Detallecolaboradores { get; set; } = new List<Detallecolaboradore>();

    [JsonIgnore]
    public virtual ICollection<Detalleestado> Detalleestados { get; set; } = new List<Detalleestado>();

    [JsonIgnore]
    public virtual Problemaequipo? IdProblemasEquiposNavigation { get; set; }

    [JsonIgnore]
    public virtual Ubicacione? IdUbicacionNavigation { get; set; }
}
