namespace AulaClara.Aplicacion.Clases.Dtos;

public class ActualizarClaseSolicitud
{
    public DateTime FechaClaseUtc { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Objetivo { get; set; } = string.Empty;

    public string? ContenidoTrabajado { get; set; }

    public string? Observaciones { get; set; }
}