using AulaClara.Dominio.Comun;
using AulaClara.Dominio.Enums;

namespace AulaClara.Dominio.Entidades;

public class Clase : EntidadBase
{
    public Guid UsuarioId { get; set; }

    public Usuario? Usuario { get; set; }

    public Guid AlumnoId { get; set; }

    public Alumno? Alumno { get; set; }

    public Guid MateriaId { get; set; }

    public Materia? Materia { get; set; }

    public DateTime FechaClaseUtc { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Objetivo { get; set; } = string.Empty;

    public string? ContenidoTrabajado { get; set; }

    public string? Observaciones { get; set; }

    public EstadoClase Estado { get; set; } = EstadoClase.Planificada;

    public List<MaterialClase> Materiales { get; set; } = new();

    public List<EjercicioClase> Ejercicios { get; set; } = new();

    public SeguimientoClase? Seguimiento { get; set; }
}