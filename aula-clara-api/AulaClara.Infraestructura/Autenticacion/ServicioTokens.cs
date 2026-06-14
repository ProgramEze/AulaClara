using AulaClara.Aplicacion.Autenticacion.Dtos;
using AulaClara.Aplicacion.Autenticacion.Interfaces;
using AulaClara.Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AulaClara.Infraestructura.Autenticacion;

public class ServicioTokens : IServicioTokens
{
    private readonly IConfiguration _configuracion;

    public ServicioTokens(IConfiguration configuracion)
    {
        _configuracion = configuracion;
    }

    public TokenGenerado GenerarToken(Usuario usuario)
    {
        var clave = _configuracion["Jwt:Clave"]
            ?? throw new InvalidOperationException("No se encontro la clave JWT.");

        var emisor = _configuracion["Jwt:Emisor"] ?? "AulaClara.Api";
        var audiencia = _configuracion["Jwt:Audiencia"] ?? "AulaClara.Cliente";

        var minutosTexto = _configuracion["Jwt:MinutosExpiracion"];
        var minutosExpiracion = int.TryParse(minutosTexto, out var minutos)
            ? minutos
            : 60;

        var expiraEnUtc = DateTime.UtcNow.AddMinutes(minutosExpiracion);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new(ClaimTypes.Name, usuario.Nombre),
            new(ClaimTypes.Email, usuario.Email)
        };

        var claveSeguridad = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(clave));

        var credenciales = new SigningCredentials(
            claveSeguridad,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: emisor,
            audience: audiencia,
            claims: claims,
            expires: expiraEnUtc,
            signingCredentials: credenciales);

        var tokenTexto = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenGenerado
        {
            Token = tokenTexto,
            ExpiraEnUtc = expiraEnUtc
        };
    }
}