using AulaClara.Aplicacion.Alumnos.Dtos;
using AulaClara.Aplicacion.Alumnos.Interfaces;
using AulaClara.Dominio.Entidades;

namespace AulaClara.Aplicacion.Alumnos.Servicios;

public class AlumnoServicio : IAlumnoServicio
{
    private readonly IRepositorioAlumnos _repositorioAlumnos;

    public AlumnoServicio(IRepositorioAlumnos repositorioAlumnos)
    {
        _repositorioAlumnos = repositorioAlumnos;
    }

    public async Task<AlumnoRespuesta> CrearAsync(Guid usuarioId, CrearAlumnoSolicitud solicitud)
    {
        var nombre = solicitud.Nombre.Trim();

        if (string.IsNullOrWhiteSpace(nombre))
            throw new InvalidOperationException("El nombre del alumno es obligatorio.");

        if (solicitud.Edad <= 0 || solicitud.Edad > 120)
            throw new InvalidOperationException("La edad del alumno debe ser mayor a 0 y menor o igual a 120.");

        var alumno = new Alumno
        {
            UsuarioId = usuarioId,
            Nombre = nombre,
            Edad = solicitud.Edad,
            Observaciones = string.IsNullOrWhiteSpace(solicitud.Observaciones)
                ? null
                : solicitud.Observaciones.Trim(),
            ResponsableNombre = string.IsNullOrWhiteSpace(solicitud.ResponsableNombre)
                ? null
                : solicitud.ResponsableNombre.Trim(),
            ResponsableContacto = string.IsNullOrWhiteSpace(solicitud.ResponsableContacto)
                ? null
                : solicitud.ResponsableContacto.Trim(),
            Activo = true
        };

        await _repositorioAlumnos.AgregarAsync(alumno);

        return MapearRespuesta(alumno);
    }

    public async Task<List<AlumnoRespuesta>> ObtenerPorUsuarioAsync(Guid usuarioId)
    {
        var alumnos = await _repositorioAlumnos.ObtenerPorUsuarioAsync(usuarioId);

        return alumnos
            .Select(MapearRespuesta)
            .ToList();
    }

    public async Task<AlumnoRespuesta?> ObtenerPorIdAsync(Guid usuarioId, Guid alumnoId)
    {
        var alumno = await _repositorioAlumnos.ObtenerPorIdYUsuarioAsync(alumnoId, usuarioId);

        if (alumno is null)
            return null;

        return MapearRespuesta(alumno);
    }

    public async Task<AlumnoRespuesta?> ActualizarAsync(
    Guid usuarioId,
    Guid alumnoId,
    ActualizarAlumnoSolicitud solicitud)
    {
        var alumno = await _repositorioAlumnos.ObtenerPorIdYUsuarioAsync(alumnoId, usuarioId);

        if (alumno is null)
            return null;

        var nombre = solicitud.Nombre.Trim();

        if (string.IsNullOrWhiteSpace(nombre))
            throw new InvalidOperationException("El nombre del alumno es obligatorio.");

        if (solicitud.Edad <= 0 || solicitud.Edad > 120)
            throw new InvalidOperationException("La edad del alumno debe ser mayor a 0 y menor o igual a 120.");

        alumno.Nombre = nombre;
        alumno.Edad = solicitud.Edad;
        alumno.Observaciones = string.IsNullOrWhiteSpace(solicitud.Observaciones)
            ? null
            : solicitud.Observaciones.Trim();
        alumno.ResponsableNombre = string.IsNullOrWhiteSpace(solicitud.ResponsableNombre)
            ? null
            : solicitud.ResponsableNombre.Trim();
        alumno.ResponsableContacto = string.IsNullOrWhiteSpace(solicitud.ResponsableContacto)
            ? null
            : solicitud.ResponsableContacto.Trim();
        alumno.FechaActualizacionUtc = DateTime.UtcNow;

        await _repositorioAlumnos.GuardarCambiosAsync();

        return MapearRespuesta(alumno);
    }

    public async Task<bool> DarDeBajaAsync(Guid usuarioId, Guid alumnoId)
    {
        var alumno = await _repositorioAlumnos.ObtenerPorIdYUsuarioAsync(alumnoId, usuarioId);

        if (alumno is null)
            return false;

        alumno.Activo = false;
        alumno.FechaActualizacionUtc = DateTime.UtcNow;

        await _repositorioAlumnos.GuardarCambiosAsync();

        return true;
    }

    private static AlumnoRespuesta MapearRespuesta(Alumno alumno)
    {
        return new AlumnoRespuesta
        {
            Id = alumno.Id,
            Nombre = alumno.Nombre,
            Edad = alumno.Edad,
            Observaciones = alumno.Observaciones,
            ResponsableNombre = alumno.ResponsableNombre,
            ResponsableContacto = alumno.ResponsableContacto,
            Activo = alumno.Activo
        };
    }
}