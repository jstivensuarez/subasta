using System.Collections.Generic;

namespace Subasta.core.dtos
{
    public class SexoDto
    {
        public int SexoId { get; set; }

        public string Descripcion { get; set; }

        public List<AnimalDto> Animales { get; set; } = new List<AnimalDto>();
    }
}