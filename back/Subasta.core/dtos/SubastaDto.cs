﻿using System;
using System.Collections.Generic;

namespace Subasta.core.dtos
{
    public class SubastaDto
    {
        public int SubastaId { get; set; }

        public string Descripcion { get; set; }

        public decimal ValorAnticipo { get; set; }

        public DateTime HoraInicio { get; set; }

        public DateTime HoraFin { get; set; }

        public DateTime HoraInicioAux { get; set; }

        public DateTime HoraFinAux { get; set; }

        public int EventoId { get; set; }

        public EventoDto Evento { get; set; }

        public bool Activo { get; set; }

        public string EstadoSolicitud { get; set; }

        public List<LoteDto> LotesDto { get; set; }

        public double TotalSegundos
        {
            get
            {
                return (HoraFin - HoraInicio).TotalSeconds;
            }
        }
    }
}