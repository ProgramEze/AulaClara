using AulaClara.Aplicacion.Materias.Dtos;

namespace AulaClara.Aplicacion.Materias.Interfaces;

public interface IMateriaServicio
{
    Task<MateriaRespuesta> CrearAsync(Guid usuarioId, CrearMateriaSolicitud solicitud);

    Task<List<MateriaRespuesta>> ObtenerPorUsuarioAsync(Guid usuarioId);

    Task<MateriaRespuesta?> ObtenerPorIdAsync(Guid usuarioId, Guid materiaId);

    Task<MateriaRespuesta?> ActualizarAsync(
        Guid usuarioId,
        Guid materiaId,
        ActualizarMateriaSolicitud solicitud);

    Task<bool> DarDeBajaAsync(Guid usuarioId, Guid materiaId);
}