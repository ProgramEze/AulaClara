using AulaClara.Aplicacion.Clases.Interfaces;
using AulaClara.Dominio.Entidades;
using AulaClara.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace AulaClara.Infraestructura.Repositorios;

public class RepositorioClases : IRepositorioClases
{
    private readonly ContextoAulaClara _contexto;

    public RepositorioClases(ContextoAulaClara contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Clase>> ObtenerPorUsuarioAsync(Guid usuarioId)
    {
        return await _contexto.Clases
            .Include(clase => clase.AlumnoMateria)
                .ThenInclude(alumnoMateria => alumnoMateria!.Alumno)
            .Include(clase => clase.AlumnoMateria)
                .ThenInclude(alumnoMateria => alumnoMateria!.Materia)
            .Where(clase => clase.UsuarioId == usuarioId)
            .OrderByDescending(clase => clase.FechaClaseUtc)
            .ToListAsync();
    }

    public async Task<Clase?> ObtenerPorIdYUsuarioAsync(Guid id, Guid usuarioId)
    {
        return await _contexto.Clases
            .Include(clase => clase.AlumnoMateria)
                .ThenInclude(alumnoMateria => alumnoMateria!.Alumno)
            .Include(clase => clase.AlumnoMateria)
                .ThenInclude(alumnoMateria => alumnoMateria!.Materia)
            .FirstOrDefaultAsync(clase =>
                clase.Id == id &&
                clase.UsuarioId == usuarioId);
    }

    public async Task AgregarAsync(Clase clase)
    {
        _contexto.Clases.Add(clase);
        await _contexto.SaveChangesAsync();
    }

    public async Task GuardarCambiosAsync()
    {
        await _contexto.SaveChangesAsync();
    }
}