using AulaClara.Aplicacion.Clases.Dtos;

namespace AulaClara.Aplicacion.Clases.Interfaces;

public interface IClaseServicio
{
    Task<ClaseRespuesta> CrearAsync(
        Guid usuarioId,
        CrearClaseSolicitud solicitud);

    Task<List<ClaseRespuesta>> ObtenerPorUsuarioAsync(Guid usuarioId);

    Task<ClaseRespuesta?> ObtenerPorIdAsync(
        Guid usuarioId,
        Guid claseId);

    Task<ClaseRespuesta?> ActualizarAsync(
        Guid usuarioId,
        Guid claseId,
        ActualizarClaseSolicitud solicitud);

    Task<ClaseRespuesta?> RealizarAsync(
        Guid usuarioId,
        Guid claseId);

    Task<ClaseRespuesta?> CancelarAsync(
        Guid usuarioId,
        Guid claseId);
}