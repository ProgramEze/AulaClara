namespace AulaClara.Aplicacion.Alumnos.Dtos;

public class CrearAlumnoSolicitud
{
    public string Nombre { get; set; } = string.Empty;

    public int Edad { get; set; }

    public string? Observaciones { get; set; }

    public string? ResponsableNombre { get; set; }

    public string? ResponsableContacto { get; set; }
}