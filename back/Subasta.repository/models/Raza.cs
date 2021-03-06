﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subasta.repository.models
{
    [Table("TBL_RAZAS")]
    public class Raza
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_RAZA")]
        public int RazaId { get; set; }

        [Column("NOMBRE_RAZA")]
        public string Descripcion { get; set; }

        public List<Animal> Animales { get; set; } = new List<Animal>();
    }
}
