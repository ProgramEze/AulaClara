using AulaClara.Dominio.Entidades;

namespace AulaClara.Aplicacion.Materias.Interfaces;

public interface IRepositorioMaterias
{
    Task<List<Materia>> ObtenerPorUsuarioAsync(Guid usuarioId);

    Task<Materia?> ObtenerPorIdYUsuarioAsync(Guid id, Guid usuarioId);

    Task<bool> ExisteNombreAsync(
        Guid usuarioId,
        string nombre,
        Guid? materiaIdIgnorada = null);

    Task AgregarAsync(Materia materia);

    Task GuardarCambiosAsync();
}