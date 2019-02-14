﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subasta.repository.models
{
    [Table("TBL_SUBASTAS")]
    public class Subasta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_SUB")]
        public int SubastaId { get; set; }

        [Column("DESCRIPCION_SUB")]
        public string Descripcion { get; set; }

        [Column("VALOR_ANTICIPO_SUB")]
        public decimal ValorAnticipo { get; set; }

        [Column("FECHA_HORA_INI_SUB")]
        public DateTime HoraInicio { get; set; }

        [Column("FECHA_LIMITE_FIN_SUB")]
        public DateTime HoraFin { get; set; }

        [Column("ACTIVO_SUB")]
        public bool Activo { get; set; }

        [Column("CODIGO_EVENTO_SUB")]
        public int EventoId { get; set; }

        public Evento Evento { get; set; }

        public List<Lote> Lotes { get; set; } = new List<Lote>();

        public List<SolicitudSubasta> SolicitudSubastas { get; set; } = new List<SolicitudSubasta>();
    }
}
