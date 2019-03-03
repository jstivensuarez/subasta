using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.dtos
{
    public class PujaGanadorDto
    {
        public PujaDto puja { get; set; }

        public ClienteDto cliente { get; set; }

        public LoteDto lote { get; set; }
    }
}
