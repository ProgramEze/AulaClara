using AulaClara.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AulaClara.Infraestructura.Repositorios;

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


        return servicios;
    }
}