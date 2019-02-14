using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.dtos
{
    public class SolicitudSubastaDto
    {
        public int SolicitudId { get; set; }
        
        public string Estado { get; set; }

        public int SubastaId { get; set; }

        public SubastaDto Subasta { get; set; }

        public string ClienteId { get; set; }

        public ClienteDto Cliente { get; set; }
    }
}
