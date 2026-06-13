using AulaClara.Dominio.Comun;

namespace AulaClara.Dominio.Entidades;

public class AlumnoMateria : EntidadBase
{
    public Guid AlumnoId { get; set; }

    public Alumno? Alumno { get; set; }

    public Guid MateriaId { get; set; }

    public Materia? Materia { get; set; }

    public DateTime FechaAsignacionUtc { get; set; } = DateTime.UtcNow;

    public bool Activa { get; set; } = true;
}