using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subasta.repository.models
{
    [Table("TBL_ANIMALES")]
    public class Animal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_ANI")]
        public int AnimalId { get; set; }

        [Column("FOTO_ANI")]
        public string Foto { get; set; }

        [Column("PESO_ANI")]
        public decimal Peso { get; set; }

        [Column("DESCRIPCION_ANI")]
        public string Descripcion { get; set; }

        [Column("COD_CATEGORIA_ANI")]
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        [Column("COD_RAZA_ANI")]
        public int RazaId { get; set; }

        public Raza Raza { get; set; }

        [Column("COD_SEXO_ANI")]
        public int SexoId { get; set; }

        public Sexo Sexo { get; set; }

        [Column("COD_MUN_PROCE_ANI")]
        public int MunicipioId { get; set; }

        public Municipio Municipio { get; set; }

        [Column("COD_LOTE_ANI")]
        public int LoteId { get; set; }

        public Lote Lote { get; set; }
    }
}
