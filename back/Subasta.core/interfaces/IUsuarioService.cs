using Subasta.core.dtos;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.interfaces
{
    public interface IUsuarioService : IGenericCrudService<UsuarioDto, Usuario>
    {
        UsuarioDto AutenticarUsuario(UsuarioDto usuario);

        void RecuperarClave(string usuario);

        void CambiarClave(UsuarioDto usuario);

        List<UsuarioDto> GetAllAdministradores();
    }
}
