using System;
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
        [MaxLength(50)]
        [Required]
        public string Descripcion { get; set; }

        [Column("VALOR_ANTICIPO_SUB")]
        [Required]
        public decimal ValorAnticipo { get; set; }

        [Column("FECHA_HORA_INI_SUB")]
        [Required]
        public DateTime HoraInicio { get; set; }

        [Column("FECHA_LIMITE_FIN_SUB")]
        [Required]
        public DateTime HoraFin { get; set; }

        [Column("ACTIVO_SUB")]
        [Required]
        public bool Activo { get; set; }

        [Column("CODIGO_EVENTO_SUB")]
        [Required]
        public int EventoId { get; set; }

        public Evento Evento { get; set; }

        public List<Lote> Lotes { get; set; } = new List<Lote>();

        public List<SolicitudSubasta> SolicitudSubastas { get; set; } = new List<SolicitudSubasta>();
    }
}
