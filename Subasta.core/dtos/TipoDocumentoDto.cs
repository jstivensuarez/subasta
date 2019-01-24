using System.Collections.Generic;

namespace Subasta.core.dtos
{
    public class TipoDocumentoDto
    {
        public int TipoDocumentoId { get; set; }

        public string Descripcion { get; set; }

        public List<ClienteDto> Clientes { get; set; } = new List<ClienteDto>();
    }
}