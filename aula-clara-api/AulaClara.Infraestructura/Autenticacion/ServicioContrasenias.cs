using AulaClara.Aplicacion.Autenticacion.Interfaces;
using AulaClara.Dominio.Entidades;
using Microsoft.AspNetCore.Identity;

namespace AulaClara.Infraestructura.Autenticacion;

public class ServicioContrasenias : IServicioContrasenias
{
    private readonly PasswordHasher<Usuario> _passwordHasher = new();

    public string GenerarHash(string contrasenia)
    {
        return _passwordHasher.HashPassword(new Usuario(), contrasenia);
    }

    public bool Verificar(string hash, string contrasenia)
    {
        var resultado = _passwordHasher.VerifyHashedPassword(
            new Usuario(),
            hash,
            contrasenia);

        return resultado == PasswordVerificationResult.Success ||
               resultado == PasswordVerificationResult.SuccessRehashNeeded;
    }
}