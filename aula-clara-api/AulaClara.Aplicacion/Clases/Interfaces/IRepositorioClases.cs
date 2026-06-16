using AulaClara.Dominio.Entidades;

namespace AulaClara.Aplicacion.Clases.Interfaces;

public interface IRepositorioClases
{
    Task<List<Clase>> ObtenerPorUsuarioAsync(Guid usuarioId);

    Task<Clase?> ObtenerPorIdYUsuarioAsync(Guid id, Guid usuarioId);

    Task AgregarAsync(Clase clase);

    Task GuardarCambiosAsync();
}