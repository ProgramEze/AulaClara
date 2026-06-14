using AulaClara.Aplicacion.Autenticacion.Interfaces;
using AulaClara.Aplicacion.Autenticacion.Servicios;
using AulaClara.Infraestructura;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AulaClara.Aplicacion.Alumnos.Servicios;
using AulaClara.Aplicacion.Alumnos.Interfaces;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var claveJwt = builder.Configuration["Jwt:Clave"]
    ?? throw new InvalidOperationException("No se encontro la clave JWT.");

var emisorJwt = builder.Configuration["Jwt:Emisor"] ?? "AulaClara.Api";
var audienciaJwt = builder.Configuration["Jwt:Audiencia"] ?? "AulaClara.Cliente";

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones =>
    {
        opciones.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = emisorJwt,

            ValidateAudience = true,
            ValidAudience = audienciaJwt,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(claveJwt)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddScoped<IAutenticacionServicio, AutenticacionServicio>();
builder.Services.AddScoped<IAlumnoServicio, AlumnoServicio>();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

/*
{
  "email": "ezequiel@test.com",
  "contrasenia": "123456"
}

bearer: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjQyMWE4MTgyLTJiNjktNDA3OC1iNWU3LWFiNmQ4YjM0ZjM3NyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJFemVxdWllbCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImV6ZXF1aWVsQHRlc3QuY29tIiwiZXhwIjoxNzgxNDU4NjI1LCJpc3MiOiJBdWxhQ2xhcmEuQXBpIiwiYXVkIjoiQXVsYUNsYXJhLkNsaWVudGUifQ.xjM-Cb1CMQNbkSgRrN5rnF3nypMYXKxIzKvlI2oku1Y
*/