using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subasta.repository.models
{
    [Table("TBL_SEXOS")]
    public class Sexo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_SEXO")]
        public int SexoId { get; set; }

        [Column("NOMBRE_SEXO")]
        public string Descripcion { get; set; }

        public List<Animal> Animales { get; set; } = new List<Animal>();
    }
}
