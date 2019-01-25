using System.Collections.Generic;

namespace Subasta.core.dtos
{
    public class CategoriaDto
    {
        public int CategoriaId { get; set; }

        public string Descripcion { get; set; }

        public List<AnimalDto> Animales { get; set; } = new List<AnimalDto>();
    }
}