using AulaClara.Aplicacion.AlumnoMaterias.Dtos;

namespace AulaClara.Aplicacion.AlumnoMaterias.Interfaces;

public interface IAlumnoMateriaServicio
{
    Task<AlumnoMateriaRespuesta> AsociarAsync(
        Guid usuarioId,
        AsociarAlumnoMateriaSolicitud solicitud);

    Task<List<AlumnoMateriaRespuesta>?> ObtenerMateriasDeAlumnoAsync(
        Guid usuarioId,
        Guid alumnoId);

    Task<List<AlumnoMateriaRespuesta>?> ObtenerAlumnosDeMateriaAsync(
        Guid usuarioId,
        Guid materiaId);

    Task<bool> DarDeBajaAsync(Guid usuarioId, Guid alumnoMateriaId);
}