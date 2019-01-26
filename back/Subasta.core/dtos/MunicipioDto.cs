using System.Collections.Generic;

namespace Subasta.core.dtos
{
    public class MunicipioDto
    {
        public int MunicipioId { get; set; }

        public string Descripcion { get; set; }

        public int DepartamentoId { get; set; }

        public DepartamentoDto Departamento { get; set; }
    }
}