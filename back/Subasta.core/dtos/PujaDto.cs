using System;

namespace Subasta.core.dtos
{
    public class PujaDto
    {
        public int PujaId { get; set; }

        public DateTime HoraPuja { get; set; }

        public decimal Valor { get; set; }

        public int PujadorId { get; set; }

        public PujadorDto Pujador { get; set; }

        public int LoteId { get; set; }

        public string Usuario { get; set; }
    }
}