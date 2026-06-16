using AulaClara.Aplicacion.Clases.Dtos;
using AulaClara.Aplicacion.Clases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AulaClara.Api.Controladores;

[ApiController]
[Authorize]
[Route("api/clases")]
public class ClasesControlador : ControllerBase
{
    private readonly IClaseServicio _claseServicio;

    public ClasesControlador(IClaseServicio claseServicio)
    {
        _claseServicio = claseServicio;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(CrearClaseSolicitud solicitud)
    {
        try
        {
            var usuarioId = ObtenerUsuarioIdDesdeToken();

            var respuesta = await _claseServicio.CrearAsync(
                usuarioId,
                solicitud);

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

        var respuesta = await _claseServicio.ObtenerPorUsuarioAsync(usuarioId);

        return Ok(respuesta);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObtenerPorId(Guid id)
    {
        var usuarioId = ObtenerUsuarioIdDesdeToken();

        var respuesta = await _claseServicio.ObtenerPorIdAsync(
            usuarioId,
            id);

        if (respuesta is null)
            return NotFound(new
            {
                mensaje = "No se encontro la clase solicitada."
            });

        return Ok(respuesta);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Actualizar(
        Guid id,
        ActualizarClaseSolicitud solicitud)
    {
        try
        {
            var usuarioId = ObtenerUsuarioIdDesdeToken();

            var respuesta = await _claseServicio.ActualizarAsync(
                usuarioId,
                id,
                solicitud);

            if (respuesta is null)
                return NotFound(new
                {
                    mensaje = "No se encontro la clase solicitada."
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

    [HttpPatch("{id:guid}/realizar")]
    public async Task<IActionResult> Realizar(Guid id)
    {
        try
        {
            var usuarioId = ObtenerUsuarioIdDesdeToken();

            var respuesta = await _claseServicio.RealizarAsync(
                usuarioId,
                id);

            if (respuesta is null)
                return NotFound(new
                {
                    mensaje = "No se encontro la clase solicitada."
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

    [HttpPatch("{id:guid}/cancelar")]
    public async Task<IActionResult> Cancelar(Guid id)
    {
        try
        {
            var usuarioId = ObtenerUsuarioIdDesdeToken();

            var respuesta = await _claseServicio.CancelarAsync(
                usuarioId,
                id);

            if (respuesta is null)
                return NotFound(new
                {
                    mensaje = "No se encontro la clase solicitada."
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

    private Guid ObtenerUsuarioIdDesdeToken()
    {
        var usuarioIdTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(usuarioIdTexto, out var usuarioId))
            throw new InvalidOperationException("No se pudo identificar al usuario autenticado.");

        return usuarioId;
    }
}