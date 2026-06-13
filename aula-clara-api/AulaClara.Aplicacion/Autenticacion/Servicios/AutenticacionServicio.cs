using AulaClara.Aplicacion.Autenticacion.Dtos;
using AulaClara.Aplicacion.Autenticacion.Interfaces;
using AulaClara.Dominio.Entidades;

namespace AulaClara.Aplicacion.Autenticacion.Servicios;

public class AutenticacionServicio : IAutenticacionServicio
{
    private readonly IRepositorioUsuarios _repositorioUsuarios;
    private readonly IServicioContrasenias _servicioContrasenias;

    public AutenticacionServicio(
        IRepositorioUsuarios repositorioUsuarios,
        IServicioContrasenias servicioContrasenias)
    {
        _repositorioUsuarios = repositorioUsuarios;
        _servicioContrasenias = servicioContrasenias;
    }

    public async Task<UsuarioRegistradoRespuesta> RegistrarAsync(RegistrarUsuarioSolicitud solicitud)
    {
        var nombre = solicitud.Nombre.Trim();
        var email = solicitud.Email.Trim().ToLower();
        var contrasenia = solicitud.Contrasenia;

        if (string.IsNullOrWhiteSpace(nombre))
            throw new InvalidOperationException("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(email))
            throw new InvalidOperationException("El email es obligatorio.");

        if (string.IsNullOrWhiteSpace(contrasenia) || contrasenia.Length < 6)
            throw new InvalidOperationException("La contrasenia debe tener al menos 6 caracteres.");

        var existeEmail = await _repositorioUsuarios.ExisteEmailAsync(email);

        if (existeEmail)
            throw new InvalidOperationException("Ya existe un usuario registrado con ese email.");

        var usuario = new Usuario
        {
            Nombre = nombre,
            Email = email,
            HashContrasenia = _servicioContrasenias.GenerarHash(contrasenia),
            Activo = true
        };

        await _repositorioUsuarios.AgregarAsync(usuario);

        return new UsuarioRegistradoRespuesta
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Email = usuario.Email
        };
    }
}