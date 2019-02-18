using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.interfaces
{
    public interface IUsuarioRepository: IGenericRepository<Usuario>
    {
        Usuario AutenticarUsuario(string usuario, string correo, string pass);

        List<Usuario> GetllWithInclude();
    }
}
