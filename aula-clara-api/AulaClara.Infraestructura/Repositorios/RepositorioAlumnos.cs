using AulaClara.Aplicacion.Alumnos.Interfaces;
using AulaClara.Dominio.Entidades;
using AulaClara.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace AulaClara.Infraestructura.Repositorios;

public class RepositorioAlumnos : IRepositorioAlumnos
{
    private readonly ContextoAulaClara _contexto;

    public RepositorioAlumnos(ContextoAulaClara contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Alumno>> ObtenerPorUsuarioAsync(Guid usuarioId)
    {
        return await _contexto.Alumnos
            .Where(alumno => alumno.UsuarioId == usuarioId && alumno.Activo)
            .OrderBy(alumno => alumno.Nombre)
            .ToListAsync();
    }

    public async Task<Alumno?> ObtenerPorIdYUsuarioAsync(Guid id, Guid usuarioId)
    {
        return await _contexto.Alumnos
            .FirstOrDefaultAsync(alumno =>
                alumno.Id == id &&
                alumno.UsuarioId == usuarioId &&
                alumno.Activo);
    }

    public async Task AgregarAsync(Alumno alumno)
    {
        _contexto.Alumnos.Add(alumno);
        await _contexto.SaveChangesAsync();
    }

    public async Task GuardarCambiosAsync()
    {
        await _contexto.SaveChangesAsync();
    }
}