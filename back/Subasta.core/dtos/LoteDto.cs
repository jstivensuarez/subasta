using System.Collections.Generic;

namespace Subasta.core.dtos
{
    public class LoteDto
    {
        public int LoteId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int CantidadElementos { get; set; }

        public decimal PesoTotal { get; set; }

        public decimal PrecioBase { get; set; }

        public decimal FotoLote { get; set; }

        public string ClienteId { get; set; }

        public ClienteDto Cliente { get; set; }

        public string MunicipioId { get; set; }

        public MunicipioDto Municipio { get; set; }

        public string SubastaId { get; set; }

        public SubastaDto Subasta { get; set; }
    }
}