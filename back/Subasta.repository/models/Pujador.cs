using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subasta.repository.models
{
    [Table("TBL_PUJADORES")]
    public class Pujador
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_LOTE_PUJADOR")]
        public int LoteId { get; set; }

        public Lote Lote { get; set; }

        [Column("ID_CLI_PUJADOR")]
        public string ClienteId { get; set; }

        public Cliente Cliente { get; set; }

        [Column("NRO_CONSIGNACION_PUJADOR")]
        public string NumeroConsignacion { get; set; }

        [Column("BANCO_CONSIGNACION_PUJADOR")]
        public string Banco { get; set; }

        [Column("VALOR_CONSIGNACION_PUJADOR")]
        public decimal ValorConsignacion { get; set; }

        [Column("ESTADO_PUJADOR")]
        public string Estado { get; set; }

        public List<Puja> Pujas { get; set; } = new List<Puja>();
    }
}