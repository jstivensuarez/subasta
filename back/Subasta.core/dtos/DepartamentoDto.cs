using System.Collections.Generic;

namespace Subasta.core.dtos
{

    public class DepartamentoDto
    {

        public int DepartamentoId { get; set; }

        public string Descripcion { get; set; }

        public List<MunicipioDto> Municipios { get; set; } = new List<MunicipioDto>();
    }
}
