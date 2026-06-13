using AulaClara.Dominio.Comun;

namespace AulaClara.Dominio.Entidades;

public class Alumno : EntidadBase
{
    public Guid UsuarioId { get; set; }

    public Usuario? Usuario { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public int Edad { get; set; }

    public string? Observaciones { get; set; }

    public string? ResponsableNombre { get; set; }

    public string? ResponsableContacto { get; set; }

    public bool Activo { get; set; } = true;

    public List<AlumnoMateria> AlumnoMaterias { get; set; } = new();

    public List<Clase> Clases { get; set; } = new();
}