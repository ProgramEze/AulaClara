using AulaClara.Dominio.Comun;

namespace AulaClara.Dominio.Entidades;

public class SeguimientoClase : EntidadBase
{
    public Guid ClaseId { get; set; }

    public Clase? Clase { get; set; }

    public string? Participacion { get; set; }

    public string? Dificultades { get; set; }

    public string? Avances { get; set; }

    public string? ProximoPaso { get; set; }
}