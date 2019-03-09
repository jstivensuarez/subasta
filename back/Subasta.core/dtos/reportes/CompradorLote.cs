using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.dtos.reportes
{
    public class CompradorLote
    {
        public string NombreComprador { get; set; }
        public int CantidadPujas { get; set; }
        public string PujaMayor { get; set; }
    }
}
