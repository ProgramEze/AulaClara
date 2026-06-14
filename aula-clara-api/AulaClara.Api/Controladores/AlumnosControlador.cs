using AulaClara.Aplicacion.Alumnos.Dtos;
using AulaClara.Aplicacion.Alumnos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AulaClara.Api.Controladores;

[ApiController]
[Authorize]
[Route("api/alumnos")]
public class AlumnosControlador : ControllerBase
{
    private readonly IAlumnoServicio _alumnoServicio;

    public AlumnosControlador(IAlumnoServicio alumnoServicio)
    {
        _alumnoServicio = alumnoServicio;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(CrearAlumnoSolicitud solicitud)
    {
        try
        {
            var usuarioId = ObtenerUsuarioIdDesdeToken();

            var respuesta = await _alumnoServicio.CrearAsync(usuarioId, solicitud);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = respuesta.Id },
                respuesta);
        }
        catch (InvalidOperationException excepcion)
        {
            return BadRequest(new
            {
                mensaje = excepcion.Message
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var usuarioId = ObtenerUsuarioIdDesdeToken();

        var respuesta = await _alumnoServicio.ObtenerPorUsuarioAsync(usuarioId);

        return Ok(respuesta);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObtenerPorId(Guid id)
    {
        var usuarioId = ObtenerUsuarioIdDesdeToken();

        var respuesta = await _alumnoServicio.ObtenerPorIdAsync(usuarioId, id);

        if (respuesta is null)
            return NotFound(new
            {
                mensaje = "No se encontro el alumno solicitado."
            });

        return Ok(respuesta);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Actualizar(Guid id, ActualizarAlumnoSolicitud solicitud)
    {
        try
        {
            var usuarioId = ObtenerUsuarioIdDesdeToken();

            var respuesta = await _alumnoServicio.ActualizarAsync(usuarioId, id, solicitud);

            if (respuesta is null)
                return NotFound(new
                {
                    mensaje = "No se encontro el alumno solicitado."
                });

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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DarDeBaja(Guid id)
    {
        var usuarioId = ObtenerUsuarioIdDesdeToken();

        var bajaRealizada = await _alumnoServicio.DarDeBajaAsync(usuarioId, id);

        if (!bajaRealizada)
            return NotFound(new
            {
                mensaje = "No se encontro el alumno solicitado."
            });

        return NoContent();
    }

    private Guid ObtenerUsuarioIdDesdeToken()
    {
        var usuarioIdTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(usuarioIdTexto, out var usuarioId))
            throw new InvalidOperationException("No se pudo identificar al usuario autenticado.");

        return usuarioId;
    }
}