namespace AulaClara.Aplicacion.Materias.Dtos;

public class MateriaRespuesta
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public bool Activa { get; set; }
}