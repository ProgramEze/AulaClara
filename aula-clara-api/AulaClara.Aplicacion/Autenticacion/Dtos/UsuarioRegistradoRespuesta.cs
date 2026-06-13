namespace AulaClara.Aplicacion.Autenticacion.Dtos;

public class UsuarioRegistradoRespuesta
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}