namespace AulaClara.Aplicacion.Alumnos.Dtos;

public class AlumnoRespuesta
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public int Edad { get; set; }

    public string? Observaciones { get; set; }

    public string? ResponsableNombre { get; set; }

    public string? ResponsableContacto { get; set; }

    public bool Activo { get; set; }
}