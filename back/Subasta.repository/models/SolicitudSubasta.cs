using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subasta.repository.models
{
    [Table("TBL_SOLICITUDES")]
    public class SolicitudSubasta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_SOLI")]
        public int SolicitudId { get; set; }

        [Column("ESTADO_SOLI")]
        public string Estado { get; set; }

        [Column("SUBASTA_SOLI")]
        public int SubastaId { get; set; }

        public Subasta Subasta { get; set; }

        [Column("CLIENTE_SOLI")]
        public string ClienteId { get; set; }

        public Cliente Cliente { get; set; }

    }
}
