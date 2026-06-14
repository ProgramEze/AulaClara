using AulaClara.Aplicacion.Materias.Dtos;
using AulaClara.Aplicacion.Materias.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AulaClara.Api.Controladores;

[ApiController]
[Authorize]
[Route("api/materias")]
public class MateriasControlador : ControllerBase
{
    private readonly IMateriaServicio _materiaServicio;

    public MateriasControlador(IMateriaServicio materiaServicio)
    {
        _materiaServicio = materiaServicio;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(CrearMateriaSolicitud solicitud)
    {
        try
        {
            var usuarioId = ObtenerUsuarioIdDesdeToken();

            var respuesta = await _materiaServicio.CrearAsync(usuarioId, solicitud);

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
    public async Task<IActionResult> ObtenerTodas()
    {
        var usuarioId = ObtenerUsuarioIdDesdeToken();

        var respuesta = await _materiaServicio.ObtenerPorUsuarioAsync(usuarioId);

        return Ok(respuesta);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObtenerPorId(Guid id)
    {
        var usuarioId = ObtenerUsuarioIdDesdeToken();

        var respuesta = await _materiaServicio.ObtenerPorIdAsync(usuarioId, id);

        if (respuesta is null)
            return NotFound(new
            {
                mensaje = "No se encontro la materia solicitada."
            });

        return Ok(respuesta);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Actualizar(Guid id, ActualizarMateriaSolicitud solicitud)
    {
        try
        {
            var usuarioId = ObtenerUsuarioIdDesdeToken();

            var respuesta = await _materiaServicio.ActualizarAsync(usuarioId, id, solicitud);

            if (respuesta is null)
                return NotFound(new
                {
                    mensaje = "No se encontro la materia solicitada."
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

        var bajaRealizada = await _materiaServicio.DarDeBajaAsync(usuarioId, id);

        if (!bajaRealizada)
            return NotFound(new
            {
                mensaje = "No se encontro la materia solicitada."
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