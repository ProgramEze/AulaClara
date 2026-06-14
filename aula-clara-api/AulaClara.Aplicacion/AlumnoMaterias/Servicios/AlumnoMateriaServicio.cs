using AulaClara.Aplicacion.AlumnoMaterias.Dtos;
using AulaClara.Aplicacion.AlumnoMaterias.Interfaces;
using AulaClara.Aplicacion.Alumnos.Interfaces;
using AulaClara.Aplicacion.Materias.Interfaces;
using AulaClara.Dominio.Entidades;

namespace AulaClara.Aplicacion.AlumnoMaterias.Servicios;

public class AlumnoMateriaServicio : IAlumnoMateriaServicio
{
    private readonly IRepositorioAlumnoMaterias _repositorioAlumnoMaterias;
    private readonly IRepositorioAlumnos _repositorioAlumnos;
    private readonly IRepositorioMaterias _repositorioMaterias;

    public AlumnoMateriaServicio(
        IRepositorioAlumnoMaterias repositorioAlumnoMaterias,
        IRepositorioAlumnos repositorioAlumnos,
        IRepositorioMaterias repositorioMaterias)
    {
        _repositorioAlumnoMaterias = repositorioAlumnoMaterias;
        _repositorioAlumnos = repositorioAlumnos;
        _repositorioMaterias = repositorioMaterias;
    }

    public async Task<AlumnoMateriaRespuesta> AsociarAsync(
        Guid usuarioId,
        AsociarAlumnoMateriaSolicitud solicitud)
    {
        var alumno = await _repositorioAlumnos.ObtenerPorIdYUsuarioAsync(
            solicitud.AlumnoId,
            usuarioId);

        if (alumno is null)
            throw new InvalidOperationException("No se encontro el alumno solicitado.");

        var materia = await _repositorioMaterias.ObtenerPorIdYUsuarioAsync(
            solicitud.MateriaId,
            usuarioId);

        if (materia is null)
            throw new InvalidOperationException("No se encontro la materia solicitada.");

        var asociacionExistente = await _repositorioAlumnoMaterias
            .ObtenerPorAlumnoYMateriaAsync(alumno.Id, materia.Id);

        if (asociacionExistente is not null && asociacionExistente.Activa)
            throw new InvalidOperationException("El alumno ya esta asociado a esa materia.");

        if (asociacionExistente is not null && !asociacionExistente.Activa)
        {
            asociacionExistente.Activa = true;
            asociacionExistente.FechaAsignacionUtc = DateTime.UtcNow;
            asociacionExistente.FechaActualizacionUtc = DateTime.UtcNow;

            await _repositorioAlumnoMaterias.GuardarCambiosAsync();

            asociacionExistente.Alumno = alumno;
            asociacionExistente.Materia = materia;

            return MapearRespuesta(asociacionExistente);
        }

        var alumnoMateria = new AlumnoMateria
        {
            AlumnoId = alumno.Id,
            Alumno = alumno,
            MateriaId = materia.Id,
            Materia = materia,
            FechaAsignacionUtc = DateTime.UtcNow,
            Activa = true
        };

        await _repositorioAlumnoMaterias.AgregarAsync(alumnoMateria);

        return MapearRespuesta(alumnoMateria);
    }

    public async Task<List<AlumnoMateriaRespuesta>?> ObtenerMateriasDeAlumnoAsync(
        Guid usuarioId,
        Guid alumnoId)
    {
        var alumno = await _repositorioAlumnos.ObtenerPorIdYUsuarioAsync(alumnoId, usuarioId);

        if (alumno is null)
            return null;

        var asociaciones = await _repositorioAlumnoMaterias.ObtenerPorAlumnoAsync(
            alumnoId,
            usuarioId);

        return asociaciones
            .Select(MapearRespuesta)
            .ToList();
    }

    public async Task<List<AlumnoMateriaRespuesta>?> ObtenerAlumnosDeMateriaAsync(
        Guid usuarioId,
        Guid materiaId)
    {
        var materia = await _repositorioMaterias.ObtenerPorIdYUsuarioAsync(materiaId, usuarioId);

        if (materia is null)
            return null;

        var asociaciones = await _repositorioAlumnoMaterias.ObtenerPorMateriaAsync(
            materiaId,
            usuarioId);

        return asociaciones
            .Select(MapearRespuesta)
            .ToList();
    }

    public async Task<bool> DarDeBajaAsync(Guid usuarioId, Guid alumnoMateriaId)
    {
        var asociacion = await _repositorioAlumnoMaterias.ObtenerPorIdYUsuarioAsync(
            alumnoMateriaId,
            usuarioId);

        if (asociacion is null)
            return false;

        asociacion.Activa = false;
        asociacion.FechaActualizacionUtc = DateTime.UtcNow;

        await _repositorioAlumnoMaterias.GuardarCambiosAsync();

        return true;
    }

    private static AlumnoMateriaRespuesta MapearRespuesta(AlumnoMateria alumnoMateria)
    {
        return new AlumnoMateriaRespuesta
        {
            Id = alumnoMateria.Id,
            AlumnoId = alumnoMateria.AlumnoId,
            AlumnoNombre = alumnoMateria.Alumno?.Nombre ?? string.Empty,
            MateriaId = alumnoMateria.MateriaId,
            MateriaNombre = alumnoMateria.Materia?.Nombre ?? string.Empty,
            FechaAsignacionUtc = alumnoMateria.FechaAsignacionUtc,
            Activa = alumnoMateria.Activa
        };
    }
}