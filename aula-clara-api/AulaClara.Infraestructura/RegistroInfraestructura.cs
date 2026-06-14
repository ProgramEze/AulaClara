using AulaClara.Aplicacion.Autenticacion.Interfaces;
using AulaClara.Infraestructura.Autenticacion;
using AulaClara.Infraestructura.Persistencia;
using AulaClara.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AulaClara.Infraestructura;

public static class RegistroInfraestructura
{
    public static IServiceCollection AgregarInfraestructura(
        this IServiceCollection servicios,
        IConfiguration configuracion)
    {
        var cadenaConexion = configuracion.GetConnectionString("ConexionSQLite")
            ?? throw new InvalidOperationException(
                "No se encontro la cadena de conexion 'ConexionSQLite'.");

        servicios.AddDbContext<ContextoAulaClara>(opciones =>
        {
            opciones.UseSqlite(cadenaConexion);
        });

        servicios.AddScoped<IServicioContrasenias, ServicioContrasenias>();
        servicios.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();
        servicios.AddScoped<IServicioTokens, ServicioTokens>();

        return servicios;
    }
}