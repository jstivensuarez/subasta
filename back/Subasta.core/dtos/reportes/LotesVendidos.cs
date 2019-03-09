using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.dtos.reportes
{
    public class LotesVendidos
    {
        public string NombreVendedor { get; set; }
        public string NombreLote { get; set; }
        public string PrecioInicial { get; set; }
        public string PrecioFinal { get; set; }
    }
}
