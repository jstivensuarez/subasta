using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.dtos
{
    public class UsuarioDto
    {
        public int UsuarioId { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Clave { get; set; }

        public string ClaveChange { get; set; }

        public int RolId { get; set; }

        public RolDto Rol { get; set; }
    }
}
