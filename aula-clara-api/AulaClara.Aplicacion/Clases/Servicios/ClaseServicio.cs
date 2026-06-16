using AulaClara.Aplicacion.AlumnoMaterias.Interfaces;
using AulaClara.Aplicacion.Clases.Dtos;
using AulaClara.Aplicacion.Clases.Interfaces;
using AulaClara.Dominio.Entidades;
using AulaClara.Dominio.Enums;

namespace AulaClara.Aplicacion.Clases.Servicios;

public class ClaseServicio : IClaseServicio
{
    private readonly IRepositorioClases _repositorioClases;
    private readonly IRepositorioAlumnoMaterias _repositorioAlumnoMaterias;

    public ClaseServicio(
        IRepositorioClases repositorioClases,
        IRepositorioAlumnoMaterias repositorioAlumnoMaterias)
    {
        _repositorioClases = repositorioClases;
        _repositorioAlumnoMaterias = repositorioAlumnoMaterias;
    }

    public async Task<ClaseRespuesta> CrearAsync(
        Guid usuarioId,
        CrearClaseSolicitud solicitud)
    {
        ValidarDatosPrincipales(
            solicitud.Titulo,
            solicitud.Objetivo);

        var alumnoMateria = await _repositorioAlumnoMaterias.ObtenerPorIdYUsuarioAsync(
            solicitud.AlumnoMateriaId,
            usuarioId);

        if (alumnoMateria is null)
            throw new InvalidOperationException("No se encontro la asociacion entre alumno y materia.");

        if (alumnoMateria.Alumno is null || !alumnoMateria.Alumno.Activo)
            throw new InvalidOperationException("El alumno asociado no esta activo.");

        if (alumnoMateria.Materia is null || !alumnoMateria.Materia.Activa)
            throw new InvalidOperationException("La materia asociada no esta activa.");

        var clase = new Clase
        {
            UsuarioId = usuarioId,
            AlumnoMateriaId = alumnoMateria.Id,
            AlumnoMateria = alumnoMateria,
            FechaClaseUtc = solicitud.FechaClaseUtc,
            Titulo = solicitud.Titulo.Trim(),
            Objetivo = solicitud.Objetivo.Trim(),
            ContenidoTrabajado = solicitud.ContenidoTrabajado?.Trim(),
            Observaciones = solicitud.Observaciones?.Trim(),
            Estado = EstadoClase.Planificada
        };

        await _repositorioClases.AgregarAsync(clase);

        return MapearRespuesta(clase);
    }

    public async Task<List<ClaseRespuesta>> ObtenerPorUsuarioAsync(Guid usuarioId)
    {
        var clases = await _repositorioClases.ObtenerPorUsuarioAsync(usuarioId);

        return clases
            .Select(MapearRespuesta)
            .ToList();
    }

    public async Task<ClaseRespuesta?> ObtenerPorIdAsync(
        Guid usuarioId,
        Guid claseId)
    {
        var clase = await _repositorioClases.ObtenerPorIdYUsuarioAsync(
            claseId,
            usuarioId);

        if (clase is null)
            return null;

        return MapearRespuesta(clase);
    }

    public async Task<ClaseRespuesta?> ActualizarAsync(
        Guid usuarioId,
        Guid claseId,
        ActualizarClaseSolicitud solicitud)
    {
        ValidarDatosPrincipales(
            solicitud.Titulo,
            solicitud.Objetivo);

        var clase = await _repositorioClases.ObtenerPorIdYUsuarioAsync(
            claseId,
            usuarioId);

        if (clase is null)
            return null;

        clase.FechaClaseUtc = solicitud.FechaClaseUtc;
        clase.Titulo = solicitud.Titulo.Trim();
        clase.Objetivo = solicitud.Objetivo.Trim();
        clase.ContenidoTrabajado = solicitud.ContenidoTrabajado?.Trim();
        clase.Observaciones = solicitud.Observaciones?.Trim();
        clase.FechaActualizacionUtc = DateTime.UtcNow;

        await _repositorioClases.GuardarCambiosAsync();

        return MapearRespuesta(clase);
    }

    public async Task<ClaseRespuesta?> RealizarAsync(
        Guid usuarioId,
        Guid claseId)
    {
        var clase = await _repositorioClases.ObtenerPorIdYUsuarioAsync(
            claseId,
            usuarioId);

        if (clase is null)
            return null;

        if (clase.Estado == EstadoClase.Realizada)
            throw new InvalidOperationException("La clase ya fue marcada como realizada.");

        if (clase.Estado == EstadoClase.Cancelada)
            throw new InvalidOperationException("No se puede marcar como realizada una clase cancelada.");

        clase.Estado = EstadoClase.Realizada;
        clase.FechaActualizacionUtc = DateTime.UtcNow;

        await _repositorioClases.GuardarCambiosAsync();

        return MapearRespuesta(clase);
    }

    public async Task<ClaseRespuesta?> CancelarAsync(
        Guid usuarioId,
        Guid claseId)
    {
        var clase = await _repositorioClases.ObtenerPorIdYUsuarioAsync(
            claseId,
            usuarioId);

        if (clase is null)
            return null;

        if (clase.Estado == EstadoClase.Cancelada)
            throw new InvalidOperationException("La clase ya fue cancelada.");

        if (clase.Estado == EstadoClase.Realizada)
            throw new InvalidOperationException("No se puede cancelar una clase ya realizada.");

        clase.Estado = EstadoClase.Cancelada;
        clase.FechaActualizacionUtc = DateTime.UtcNow;

        await _repositorioClases.GuardarCambiosAsync();

        return MapearRespuesta(clase);
    }

    private static void ValidarDatosPrincipales(
        string titulo,
        string objetivo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new InvalidOperationException("El titulo de la clase es obligatorio.");

        if (string.IsNullOrWhiteSpace(objetivo))
            throw new InvalidOperationException("El objetivo de la clase es obligatorio.");
    }

    private static ClaseRespuesta MapearRespuesta(Clase clase)
    {
        return new ClaseRespuesta
        {
            Id = clase.Id,
            AlumnoMateriaId = clase.AlumnoMateriaId,
            AlumnoId = clase.AlumnoMateria?.AlumnoId ?? Guid.Empty,
            AlumnoNombre = clase.AlumnoMateria?.Alumno?.Nombre ?? string.Empty,
            MateriaId = clase.AlumnoMateria?.MateriaId ?? Guid.Empty,
            MateriaNombre = clase.AlumnoMateria?.Materia?.Nombre ?? string.Empty,
            FechaClaseUtc = clase.FechaClaseUtc,
            Titulo = clase.Titulo,
            Objetivo = clase.Objetivo,
            ContenidoTrabajado = clase.ContenidoTrabajado,
            Observaciones = clase.Observaciones,
            Estado = clase.Estado
        };
    }
}