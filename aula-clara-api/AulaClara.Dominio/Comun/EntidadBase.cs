namespace AulaClara.Dominio.Comun;

public abstract class EntidadBase
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime FechaCreacionUtc { get; set; } = DateTime.UtcNow;

    public DateTime? FechaActualizacionUtc { get; set; }
}