using AulaClara.Dominio.Comun;
using AulaClara.Dominio.Enums;

namespace AulaClara.Dominio.Entidades;

public class EjercicioClase : EntidadBase
{
    public Guid ClaseId { get; set; }

    public Clase? Clase { get; set; }

    public string Consigna { get; set; } = string.Empty;

    public string? RespuestaEsperada { get; set; }

    public NivelEjercicio Nivel { get; set; }

    public int Orden { get; set; }
}