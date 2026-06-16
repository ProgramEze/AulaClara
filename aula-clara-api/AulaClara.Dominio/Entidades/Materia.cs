using AulaClara.Dominio.Comun;

namespace AulaClara.Dominio.Entidades;

public class Materia : EntidadBase
{
    public Guid UsuarioId { get; set; }

    public Usuario? Usuario { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public bool Activa { get; set; } = true;

    public List<AlumnoMateria> AlumnoMaterias { get; set; } = new();
}