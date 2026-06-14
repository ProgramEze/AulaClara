namespace AulaClara.Aplicacion.AlumnoMaterias.Dtos;

public class AlumnoMateriaRespuesta
{
    public Guid Id { get; set; }

    public Guid AlumnoId { get; set; }

    public string AlumnoNombre { get; set; } = string.Empty;

    public Guid MateriaId { get; set; }

    public string MateriaNombre { get; set; } = string.Empty;

    public DateTime FechaAsignacionUtc { get; set; }

    public bool Activa { get; set; }
}