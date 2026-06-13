namespace AulaClara.Aplicacion.Autenticacion.Dtos;

public class RegistrarUsuarioSolicitud
{
    public string Nombre { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Contrasenia { get; set; } = string.Empty;
}