using AulaClara.Dominio.Entidades;

namespace AulaClara.Aplicacion.AlumnoMaterias.Interfaces;

public interface IRepositorioAlumnoMaterias
{
    Task<AlumnoMateria?> ObtenerPorIdYUsuarioAsync(Guid id, Guid usuarioId);

    Task<AlumnoMateria?> ObtenerPorAlumnoYMateriaAsync(Guid alumnoId, Guid materiaId);

    Task<List<AlumnoMateria>> ObtenerPorAlumnoAsync(Guid alumnoId, Guid usuarioId);

    Task<List<AlumnoMateria>> ObtenerPorMateriaAsync(Guid materiaId, Guid usuarioId);

    Task AgregarAsync(AlumnoMateria alumnoMateria);

    Task GuardarCambiosAsync();
}