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
        [Required]
        public DateTime HoraPuja { get; set; }

        [Column("VALOR_PUJA")]
        [Required]
        public decimal Valor { get; set; }

        [Column("COD_SUBASTA_PUJADOR")]
        [Required]
        public int PujadorId { get; set; }

        public Pujador Pujador { get; set; }
    }
}