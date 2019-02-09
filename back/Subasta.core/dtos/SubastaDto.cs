using System;
using System.Collections.Generic;

namespace Subasta.core.dtos
{
    public class SubastaDto
    {
        public int SubastaId { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaLimite { get; set; }

        public DateTime HoraInicio { get; set; }

        public DateTime HoraFin { get; set; }

        public DateTime HoraInicioAux { get; set; }

        public DateTime HoraFinAux { get; set; }

        public int EventoId { get; set; }

        public EventoDto Evento { get; set; }

        public bool Activo { get; set; }
    }
}