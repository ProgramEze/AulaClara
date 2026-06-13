using AulaClara.Aplicacion.Autenticacion.Dtos;

namespace AulaClara.Aplicacion.Autenticacion.Interfaces;

public interface IAutenticacionServicio
{
    Task<UsuarioRegistradoRespuesta> RegistrarAsync(RegistrarUsuarioSolicitud solicitud);
}