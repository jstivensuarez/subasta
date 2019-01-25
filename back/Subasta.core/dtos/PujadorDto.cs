using System.Collections.Generic;

namespace Subasta.core.dtos
{
    public class PujadorDto
    {
        public int SubastaId { get; set; }

        public SubastaDto Subasta { get; set; }

        public string ClienteId { get; set; }

        public ClienteDto Cliente { get; set; }

        public string NumeroConsignacion { get; set; }

        public string Banco { get; set; }

        public decimal ValorConsignacion { get; set; }

        public string Estado { get; set; }

        public List<PujaDto> Pujas { get; set; } = new List<PujaDto>();
    }
}