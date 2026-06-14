using AulaClara.Aplicacion.Alumnos.Interfaces;
using AulaClara.Aplicacion.Alumnos.Servicios;
using AulaClara.Aplicacion.Materias.Interfaces;
using AulaClara.Aplicacion.Materias.Servicios;
using AulaClara.Aplicacion.Autenticacion.Interfaces;
using AulaClara.Aplicacion.Autenticacion.Servicios;
using AulaClara.Infraestructura;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Aula Clara API",
        Version = "v1"
    });

    opciones.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingresá el token con el formato: Bearer {tu token JWT}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    opciones.AddSecurityRequirement(documento =>
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecuritySchemeReference("Bearer", documento),
                new List<string>()
            }
        });
});

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
builder.Services.AddScoped<IAlumnoServicio, AlumnoServicio>();
builder.Services.AddScoped<IMateriaServicio, MateriaServicio>();
builder.Services.AgregarInfraestructura(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(opciones =>
    {
        opciones.SwaggerEndpoint("/swagger/v1/swagger.json", "Aula Clara API v1");
    });
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();