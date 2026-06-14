namespace AulaClara.Aplicacion.Autenticacion.Dtos;

public class TokenGenerado
{
    public string Token { get; set; } = string.Empty;

    public DateTime ExpiraEnUtc { get; set; }
}