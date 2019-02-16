using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subasta.repository.models
{
    [Table("TBL_CLASIFICACIONES")]
    public class Clasificacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_CLAS")]
        public int ClasificacionId { get; set; }

        [Column("NOMBRE_CLAS")]
        public string Descripcion { get; set; }

        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        public List<Lote> Lotes { get; set; } = new List<Lote>();
    }
}
