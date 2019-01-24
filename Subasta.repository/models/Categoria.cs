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
        public string Descripcion { get; set; }

        public List<Animal> Animales { get; set; } = new List<Animal>();
    }
}
