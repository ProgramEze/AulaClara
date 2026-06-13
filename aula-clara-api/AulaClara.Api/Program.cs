using AulaClara.Aplicacion.Autenticacion.Interfaces;
using AulaClara.Aplicacion.Autenticacion.Servicios;
using AulaClara.Infraestructura;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddScoped<IAutenticacionServicio, AutenticacionServicio>();

builder.Services.AgregarInfraestructura(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(opciones =>
    {
        opciones.SwaggerEndpoint("/openapi/v1.json", "Aula Clara API v1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();