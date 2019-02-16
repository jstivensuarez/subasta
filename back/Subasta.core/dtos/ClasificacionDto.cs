using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.dtos
{
    public class ClasificacionDto
    {
        public int ClasificacionId { get; set; }

        public string Descripcion { get; set; }

        public int CategoriaId { get; set; }

        public CategoriaDto Categoria { get; set; }
    }
}
