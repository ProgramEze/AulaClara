using AulaClara.Dominio.Comun;

namespace AulaClara.Dominio.Entidades;

public class Usuario : EntidadBase
{
    public string Nombre { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string HashContrasenia { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;

    public List<Alumno> Alumnos { get; set; } = new();

    public List<Materia> Materias { get; set; } = new();

    public List<Clase> Clases { get; set; } = new();
}