using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subasta.repository.models
{
    [Table("TBL_CONFIRMACIONES")]
    public class ConfirmacionPago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_CONFIRMACION")]
        public int ConfirmacionId { get; set; }

        [Column("FECHA_CONFIRMACION")]
        [Required]
        public DateTime Fecha { get; set; }

        [Column("USUARIO_CONFIRMACION")]
        [MaxLength(50)]
        [Required]
        public string Usuario { get; set; }

        [Column("ESTADO_CONFIRMACION")]
        [MaxLength(50)]
        [Required]
        public string Estado { get; set; }

        [Column("ID_LOTE_CONFIRMACION")]
        [Required]
        public int LoteId { get; set; }

        public Lote Lote { get; set; }
    }
}
