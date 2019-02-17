﻿using System;
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
        [MaxLength(200)]
        [Required]
        public string Foto { get; set; }

        [Column("PESO_ANI")]
        [Required]
        public decimal Peso { get; set; }

        [Column("DESCRIPCION_ANI")]
        [MaxLength(2000)]
        [Required]
        public string Descripcion { get; set; }

        [Column("ACTIVO_ANI")]
        [Required]
        public bool Activo { get; set; }

        [Column("SEXO_ANI")]
        [Required]
        public string Sexo { get; set; }

        [Column("COD_MUN_PROCE_ANI")]
        [Required]
        public int MunicipioId { get; set; }

        public Municipio Municipio { get; set; }

        [Column("COD_LOTE_ANI")]
        [Required]
        public int LoteId { get; set; }

        public Lote Lote { get; set; }
    }
}
