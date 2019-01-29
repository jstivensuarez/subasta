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

        public DateTime HoraInicioAux { get; set; }

        public DateTime HoraFinAux { get; set; }

        public decimal ValorAnticipo { get; set; }

        public decimal PrecioInicial { get; set; }

        public int EventoId { get; set; }

        public EventoDto Evento { get; set; }
    }
}