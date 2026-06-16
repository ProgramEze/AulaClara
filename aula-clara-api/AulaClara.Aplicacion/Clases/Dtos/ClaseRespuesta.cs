using AulaClara.Dominio.Enums;

namespace AulaClara.Aplicacion.Clases.Dtos;

public class ClaseRespuesta
{
    public Guid Id { get; set; }

    public Guid AlumnoMateriaId { get; set; }

    public Guid AlumnoId { get; set; }

    public string AlumnoNombre { get; set; } = string.Empty;

    public Guid MateriaId { get; set; }

    public string MateriaNombre { get; set; } = string.Empty;

    public DateTime FechaClaseUtc { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Objetivo { get; set; } = string.Empty;

    public string? ContenidoTrabajado { get; set; }

    public string? Observaciones { get; set; }

    public EstadoClase Estado { get; set; }
}