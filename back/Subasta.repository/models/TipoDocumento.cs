using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subasta.repository.models
{
    [Table("TBL_TIPO_DOCUMENTOS")]
    public class TipoDocumento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_TD")]
        public int TipoDocumentoId { get; set; }

        [Column("NOMBRE_TD")]
        public string Descripcion { get; set; }

        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}
