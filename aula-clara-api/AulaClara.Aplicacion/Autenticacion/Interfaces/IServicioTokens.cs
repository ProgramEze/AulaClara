using AulaClara.Aplicacion.Autenticacion.Dtos;
using AulaClara.Dominio.Entidades;

namespace AulaClara.Aplicacion.Autenticacion.Interfaces;

public interface IServicioTokens
{
    TokenGenerado GenerarToken(Usuario usuario);
}