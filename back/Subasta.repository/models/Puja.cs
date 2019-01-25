using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subasta.repository.models
{
    [Table("TBL_PUJAS")]
    public class Puja
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_PUJA")]
        public int PujaId { get; set; }

        [Column("HORA_PUJA")]
        public DateTime HoraPuja { get; set; }

        [Column("VALOR_PUJA")]
        public decimal Valor { get; set; }

        [Column("COD_SUBASTA_PUJADOR")]
        public int PujadorId { get; set; }

        public Pujador Pujador { get; set; }
    }
}