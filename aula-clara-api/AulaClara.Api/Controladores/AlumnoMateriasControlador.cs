using AulaClara.Aplicacion.AlumnoMaterias.Dtos;
using AulaClara.Aplicacion.AlumnoMaterias.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AulaClara.Api.Controladores;

[ApiController]
[Authorize]
[Route("api/alumno-materias")]
public class AlumnoMateriasControlador : ControllerBase
{
    private readonly IAlumnoMateriaServicio _alumnoMateriaServicio;

    public AlumnoMateriasControlador(IAlumnoMateriaServicio alumnoMateriaServicio)
    {
        _alumnoMateriaServicio = alumnoMateriaServicio;
    }

    [HttpPost]
    public async Task<IActionResult> Asociar(AsociarAlumnoMateriaSolicitud solicitud)
    {
        try
        {
            var usuarioId = ObtenerUsuarioIdDesdeToken();

            var respuesta = await _alumnoMateriaServicio.AsociarAsync(usuarioId, solicitud);

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

    [HttpGet("alumnos/{alumnoId:guid}/materias")]
    public async Task<IActionResult> ObtenerMateriasDeAlumno(Guid alumnoId)
    {
        var usuarioId = ObtenerUsuarioIdDesdeToken();

        var respuesta = await _alumnoMateriaServicio.ObtenerMateriasDeAlumnoAsync(
            usuarioId,
            alumnoId);

        if (respuesta is null)
            return NotFound(new
            {
                mensaje = "No se encontro el alumno solicitado."
            });

        return Ok(respuesta);
    }

    [HttpGet("materias/{materiaId:guid}/alumnos")]
    public async Task<IActionResult> ObtenerAlumnosDeMateria(Guid materiaId)
    {
        var usuarioId = ObtenerUsuarioIdDesdeToken();

        var respuesta = await _alumnoMateriaServicio.ObtenerAlumnosDeMateriaAsync(
            usuarioId,
            materiaId);

        if (respuesta is null)
            return NotFound(new
            {
                mensaje = "No se encontro la materia solicitada."
            });

        return Ok(respuesta);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DarDeBaja(Guid id)
    {
        var usuarioId = ObtenerUsuarioIdDesdeToken();

        var bajaRealizada = await _alumnoMateriaServicio.DarDeBajaAsync(usuarioId, id);

        if (!bajaRealizada)
            return NotFound(new
            {
                mensaje = "No se encontro la asociacion solicitada."
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