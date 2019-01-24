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
    }
}