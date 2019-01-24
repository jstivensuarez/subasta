using System;
using System.Collections.Generic;

namespace Subasta.core.dtos
{
    public class SubastaDto
    {
        public int SubastaId { get; set; }

        public DateTime FechaLimite { get; set; }

        public DateTime HoraInicio { get; set; }

        public DateTime HoraFin { get; set; }


        public decimal PrecioInicial { get; set; }

        public int EventoId { get; set; }

        public EventoDto Evento { get; set; }

        public List<PujadorDto> Pujadores { get; set; } = new List<PujadorDto>();

        public List<LoteDto> Lotes { get; set; } = new List<LoteDto>();
    }
}