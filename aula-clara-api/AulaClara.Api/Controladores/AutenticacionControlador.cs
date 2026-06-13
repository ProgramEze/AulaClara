using AulaClara.Aplicacion.Autenticacion.Dtos;
using AulaClara.Aplicacion.Autenticacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
}