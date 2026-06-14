using AulaClara.Dominio.Entidades;

namespace AulaClara.Aplicacion.Alumnos.Interfaces;

public interface IRepositorioAlumnos
{
    Task<List<Alumno>> ObtenerPorUsuarioAsync(Guid usuarioId);

    Task<Alumno?> ObtenerPorIdYUsuarioAsync(Guid id, Guid usuarioId);

    Task AgregarAsync(Alumno alumno);
}