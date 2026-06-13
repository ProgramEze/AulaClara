using AulaClara.Dominio.Comun;
using AulaClara.Dominio.Enums;

namespace AulaClara.Dominio.Entidades;

public class MaterialClase : EntidadBase
{
    public Guid ClaseId { get; set; }

    public Clase? Clase { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public TipoMaterial Tipo { get; set; }

    public string? Url { get; set; }

    public string? Descripcion { get; set; }

    public int Orden { get; set; }
}