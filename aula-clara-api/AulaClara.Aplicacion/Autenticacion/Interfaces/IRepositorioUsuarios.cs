using AulaClara.Dominio.Entidades;

namespace AulaClara.Aplicacion.Autenticacion.Interfaces;

public interface IRepositorioUsuarios
{
    Task<bool> ExisteEmailAsync(string email);

    Task<Usuario?> ObtenerPorEmailAsync(string email);

    Task AgregarAsync(Usuario usuario);
}