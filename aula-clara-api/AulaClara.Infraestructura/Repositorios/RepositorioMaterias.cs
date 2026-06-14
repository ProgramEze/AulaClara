using AulaClara.Aplicacion.Materias.Interfaces;
using AulaClara.Dominio.Entidades;
using AulaClara.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace AulaClara.Infraestructura.Repositorios;

public class RepositorioMaterias : IRepositorioMaterias
{
    private readonly ContextoAulaClara _contexto;

    public RepositorioMaterias(ContextoAulaClara contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Materia>> ObtenerPorUsuarioAsync(Guid usuarioId)
    {
        return await _contexto.Materias
            .Where(materia => materia.UsuarioId == usuarioId && materia.Activa)
            .OrderBy(materia => materia.Nombre)
            .ToListAsync();
    }

    public async Task<Materia?> ObtenerPorIdYUsuarioAsync(Guid id, Guid usuarioId)
    {
        return await _contexto.Materias
            .FirstOrDefaultAsync(materia =>
                materia.Id == id &&
                materia.UsuarioId == usuarioId &&
                materia.Activa);
    }

    public async Task<bool> ExisteNombreAsync(
        Guid usuarioId,
        string nombre,
        Guid? materiaIdIgnorada = null)
    {
        var nombreNormalizado = nombre.ToLower();

        return await _contexto.Materias
            .AnyAsync(materia =>
                materia.UsuarioId == usuarioId &&
                materia.Nombre.ToLower() == nombreNormalizado &&
                (!materiaIdIgnorada.HasValue || materia.Id != materiaIdIgnorada.Value));
    }

    public async Task AgregarAsync(Materia materia)
    {
        _contexto.Materias.Add(materia);
        await _contexto.SaveChangesAsync();
    }

    public async Task GuardarCambiosAsync()
    {
        await _contexto.SaveChangesAsync();
    }
}