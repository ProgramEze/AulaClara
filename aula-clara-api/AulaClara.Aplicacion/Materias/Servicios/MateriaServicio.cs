using AulaClara.Aplicacion.Materias.Dtos;
using AulaClara.Aplicacion.Materias.Interfaces;
using AulaClara.Dominio.Entidades;

namespace AulaClara.Aplicacion.Materias.Servicios;

public class MateriaServicio : IMateriaServicio
{
    private readonly IRepositorioMaterias _repositorioMaterias;

    public MateriaServicio(IRepositorioMaterias repositorioMaterias)
    {
        _repositorioMaterias = repositorioMaterias;
    }

    public async Task<MateriaRespuesta> CrearAsync(Guid usuarioId, CrearMateriaSolicitud solicitud)
    {
        var nombre = solicitud.Nombre.Trim();

        if (string.IsNullOrWhiteSpace(nombre))
            throw new InvalidOperationException("El nombre de la materia es obligatorio.");

        var existeNombre = await _repositorioMaterias.ExisteNombreAsync(usuarioId, nombre);

        if (existeNombre)
            throw new InvalidOperationException("Ya existe una materia con ese nombre.");

        var materia = new Materia
        {
            UsuarioId = usuarioId,
            Nombre = nombre,
            Descripcion = string.IsNullOrWhiteSpace(solicitud.Descripcion)
                ? null
                : solicitud.Descripcion.Trim(),
            Activa = true
        };

        await _repositorioMaterias.AgregarAsync(materia);

        return MapearRespuesta(materia);
    }

    public async Task<List<MateriaRespuesta>> ObtenerPorUsuarioAsync(Guid usuarioId)
    {
        var materias = await _repositorioMaterias.ObtenerPorUsuarioAsync(usuarioId);

        return materias
            .Select(MapearRespuesta)
            .ToList();
    }

    public async Task<MateriaRespuesta?> ObtenerPorIdAsync(Guid usuarioId, Guid materiaId)
    {
        var materia = await _repositorioMaterias.ObtenerPorIdYUsuarioAsync(materiaId, usuarioId);

        if (materia is null)
            return null;

        return MapearRespuesta(materia);
    }

    public async Task<MateriaRespuesta?> ActualizarAsync(
        Guid usuarioId,
        Guid materiaId,
        ActualizarMateriaSolicitud solicitud)
    {
        var materia = await _repositorioMaterias.ObtenerPorIdYUsuarioAsync(materiaId, usuarioId);

        if (materia is null)
            return null;

        var nombre = solicitud.Nombre.Trim();

        if (string.IsNullOrWhiteSpace(nombre))
            throw new InvalidOperationException("El nombre de la materia es obligatorio.");

        var existeNombre = await _repositorioMaterias.ExisteNombreAsync(
            usuarioId,
            nombre,
            materiaId);

        if (existeNombre)
            throw new InvalidOperationException("Ya existe otra materia con ese nombre.");

        materia.Nombre = nombre;
        materia.Descripcion = string.IsNullOrWhiteSpace(solicitud.Descripcion)
            ? null
            : solicitud.Descripcion.Trim();
        materia.FechaActualizacionUtc = DateTime.UtcNow;

        await _repositorioMaterias.GuardarCambiosAsync();

        return MapearRespuesta(materia);
    }

    public async Task<bool> DarDeBajaAsync(Guid usuarioId, Guid materiaId)
    {
        var materia = await _repositorioMaterias.ObtenerPorIdYUsuarioAsync(materiaId, usuarioId);

        if (materia is null)
            return false;

        materia.Activa = false;
        materia.FechaActualizacionUtc = DateTime.UtcNow;

        await _repositorioMaterias.GuardarCambiosAsync();

        return true;
    }

    private static MateriaRespuesta MapearRespuesta(Materia materia)
    {
        return new MateriaRespuesta
        {
            Id = materia.Id,
            Nombre = materia.Nombre,
            Descripcion = materia.Descripcion,
            Activa = materia.Activa
        };
    }
}