using System;
using System.Collections.Generic;

namespace Subasta.core.dtos
{

    public class EventoDto
    {

        public int EventoId { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public int MunicipioId { get; set; }

        public MunicipioDto Municipio { get; set; }

        public bool Activo { get; set; }

        public bool Publicado { get; set; }

        public List<SubastaDto> SubastasDto { get; set; }
    }
}
