using AulaClara.Aplicacion.Autenticacion.Interfaces;
using AulaClara.Dominio.Entidades;
using AulaClara.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace AulaClara.Infraestructura.Repositorios;

public class RepositorioUsuarios : IRepositorioUsuarios
{
    private readonly ContextoAulaClara _contexto;

    public RepositorioUsuarios(ContextoAulaClara contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> ExisteEmailAsync(string email)
    {
        return await _contexto.Usuarios
            .AnyAsync(usuario => usuario.Email == email);
    }

    public async Task AgregarAsync(Usuario usuario)
    {
        _contexto.Usuarios.Add(usuario);
        await _contexto.SaveChangesAsync();
    }
}