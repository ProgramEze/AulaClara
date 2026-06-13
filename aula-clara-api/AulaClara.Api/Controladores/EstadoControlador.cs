using Microsoft.AspNetCore.Mvc;

namespace AulaClara.Api.Controladores;

[ApiController]
[Route("api/estado")]
public class EstadoControlador : ControllerBase
{
    [HttpGet]
    public IActionResult ObtenerEstado()
    {
        var respuesta = new
        {
            nombre = "Aula Clara API",
            estado = "Funcionando",
            version = "1.0.0",
            entorno = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "No definido",
            fechaUtc = DateTime.UtcNow
        };

        return Ok(respuesta);
    }
}