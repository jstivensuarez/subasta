using System.Collections.Generic;

namespace Subasta.core.dtos
{
    public class RazaDto
    {
        public int RazaId { get; set; }

        public string Descripcion { get; set; }

        public int CategoriaId { get; set; }

        public CategoriaDto Categoria { get; set; }

    }
}