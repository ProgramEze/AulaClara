namespace AulaClara.Aplicacion.Autenticacion.Dtos;

public class IniciarSesionSolicitud
{
    public string Email { get; set; } = string.Empty;

    public string Contrasenia { get; set; } = string.Empty;
}