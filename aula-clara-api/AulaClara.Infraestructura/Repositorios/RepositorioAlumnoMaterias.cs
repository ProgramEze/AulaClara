using AulaClara.Aplicacion.AlumnoMaterias.Interfaces;
using AulaClara.Dominio.Entidades;
using AulaClara.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace AulaClara.Infraestructura.Repositorios;

public class RepositorioAlumnoMaterias : IRepositorioAlumnoMaterias
{
    private readonly ContextoAulaClara _contexto;

    public RepositorioAlumnoMaterias(ContextoAulaClara contexto)
    {
        _contexto = contexto;
    }

    public async Task<AlumnoMateria?> ObtenerPorIdYUsuarioAsync(Guid id, Guid usuarioId)
    {
        return await _contexto.AlumnoMaterias
            .Include(alumnoMateria => alumnoMateria.Alumno)
            .Include(alumnoMateria => alumnoMateria.Materia)
            .FirstOrDefaultAsync(alumnoMateria =>
                alumnoMateria.Id == id &&
                alumnoMateria.Activa &&
                alumnoMateria.Alumno != null &&
                alumnoMateria.Alumno.UsuarioId == usuarioId &&
                alumnoMateria.Materia != null &&
                alumnoMateria.Materia.UsuarioId == usuarioId);
    }

    public async Task<AlumnoMateria?> ObtenerPorAlumnoYMateriaAsync(Guid alumnoId, Guid materiaId)
    {
        return await _contexto.AlumnoMaterias
            .Include(alumnoMateria => alumnoMateria.Alumno)
            .Include(alumnoMateria => alumnoMateria.Materia)
            .FirstOrDefaultAsync(alumnoMateria =>
                alumnoMateria.AlumnoId == alumnoId &&
                alumnoMateria.MateriaId == materiaId);
    }

    public async Task<List<AlumnoMateria>> ObtenerPorAlumnoAsync(Guid alumnoId, Guid usuarioId)
    {
        return await _contexto.AlumnoMaterias
            .Include(alumnoMateria => alumnoMateria.Alumno)
            .Include(alumnoMateria => alumnoMateria.Materia)
            .Where(alumnoMateria =>
                alumnoMateria.AlumnoId == alumnoId &&
                alumnoMateria.Activa &&
                alumnoMateria.Alumno != null &&
                alumnoMateria.Alumno.UsuarioId == usuarioId &&
                alumnoMateria.Materia != null &&
                alumnoMateria.Materia.UsuarioId == usuarioId &&
                alumnoMateria.Materia.Activa)
            .OrderBy(alumnoMateria => alumnoMateria.Materia!.Nombre)
            .ToListAsync();
    }

    public async Task<List<AlumnoMateria>> ObtenerPorMateriaAsync(Guid materiaId, Guid usuarioId)
    {
        return await _contexto.AlumnoMaterias
            .Include(alumnoMateria => alumnoMateria.Alumno)
            .Include(alumnoMateria => alumnoMateria.Materia)
            .Where(alumnoMateria =>
                alumnoMateria.MateriaId == materiaId &&
                alumnoMateria.Activa &&
                alumnoMateria.Alumno != null &&
                alumnoMateria.Alumno.UsuarioId == usuarioId &&
                alumnoMateria.Alumno.Activo &&
                alumnoMateria.Materia != null &&
                alumnoMateria.Materia.UsuarioId == usuarioId)
            .OrderBy(alumnoMateria => alumnoMateria.Alumno!.Nombre)
            .ToListAsync();
    }

    public async Task AgregarAsync(AlumnoMateria alumnoMateria)
    {
        _contexto.AlumnoMaterias.Add(alumnoMateria);
        await _contexto.SaveChangesAsync();
    }

    public async Task GuardarCambiosAsync()
    {
        await _contexto.SaveChangesAsync();
    }
}