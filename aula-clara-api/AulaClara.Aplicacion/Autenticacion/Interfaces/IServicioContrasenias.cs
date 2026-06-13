namespace AulaClara.Aplicacion.Autenticacion.Interfaces;

public interface IServicioContrasenias
{
    string GenerarHash(string contrasenia);

    bool Verificar(string hash, string contrasenia);
}