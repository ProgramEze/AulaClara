using AulaClara.Aplicacion.Autenticacion.Interfaces;
using AulaClara.Aplicacion.Autenticacion.Servicios;
using AulaClara.Infraestructura;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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