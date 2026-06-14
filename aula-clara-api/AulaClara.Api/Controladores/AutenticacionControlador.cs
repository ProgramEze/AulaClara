using AulaClara.Aplicacion.Autenticacion.Dtos;
using AulaClara.Aplicacion.Autenticacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AulaClara.Api.Controladores;

[ApiController]
[Route("api/autenticacion")]
public class AutenticacionControlador : ControllerBase
{
    private readonly IAutenticacionServicio _autenticacionServicio;

    public AutenticacionControlador(IAutenticacionServicio autenticacionServicio)
    {
        _autenticacionServicio = autenticacionServicio;
    }

    [HttpPost("registro")]
    public async Task<IActionResult> Registrar(RegistrarUsuarioSolicitud solicitud)
    {
        try
        {
            var respuesta = await _autenticacionServicio.RegistrarAsync(solicitud);

            return Created(string.Empty, respuesta);
        }
        catch (InvalidOperationException excepcion)
        {
            return BadRequest(new
            {
                mensaje = excepcion.Message
            });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> IniciarSesion(IniciarSesionSolicitud solicitud)
    {
        try
        {
            var respuesta = await _autenticacionServicio.IniciarSesionAsync(solicitud);

            return Ok(respuesta);
        }
        catch (InvalidOperationException excepcion)
        {
            return BadRequest(new
            {
                mensaje = excepcion.Message
            });
        }
    }

    [Authorize]
    [HttpGet("perfil")]
    public IActionResult ObtenerPerfil()
    {
        var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var nombre = User.FindFirstValue(ClaimTypes.Name);
        var email = User.FindFirstValue(ClaimTypes.Email);

        return Ok(new
        {
            id = usuarioId,
            nombre,
            email
        });
    }
}