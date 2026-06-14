using AulaClara.Aplicacion.Alumnos.Dtos;

namespace AulaClara.Aplicacion.Alumnos.Interfaces;

public interface IAlumnoServicio
{
    Task<AlumnoRespuesta> CrearAsync(Guid usuarioId, CrearAlumnoSolicitud solicitud);

    Task<List<AlumnoRespuesta>> ObtenerPorUsuarioAsync(Guid usuarioId);

    Task<AlumnoRespuesta?> ObtenerPorIdAsync(Guid usuarioId, Guid alumnoId);
}