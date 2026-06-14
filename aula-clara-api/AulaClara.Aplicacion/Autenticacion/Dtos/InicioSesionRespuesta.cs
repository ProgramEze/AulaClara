namespace AulaClara.Aplicacion.Autenticacion.Dtos;

public class InicioSesionRespuesta
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;

    public DateTime ExpiraEnUtc { get; set; }
}