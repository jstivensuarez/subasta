using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subasta.repository.models
{
    [Table("TBL_CATEGORIAS")]
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_CAT")]
        public int CategoriaId { get; set; }

        [Column("NOMBRE_CAT")]
        [MaxLength(50)]
        [Required]
        public string Descripcion { get; set; }

        public List<Raza> Razas { get; set; } = new List<Raza>();

        public List<Lote> Lotes { get; set; } = new List<Lote>();

        public List<Clasificacion> Clasificaciones { get; set; } = new List<Clasificacion>();
    }
}
