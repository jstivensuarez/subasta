using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subasta.repository.models
{
    [Table("TBL_EVENTOS")]
    public class Evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_EVEN")]
        public int EventoId { get; set; }

        [Column("NOMBRE_EVEN")]
        [MaxLength(50)]
        [Required]
        public string Descripcion { get; set; }

        [Column("FECHA_INI_EVEN")]
        [Required]
        public DateTime FechaInicio { get; set; }

        [Column("FECHA_FIN_EVEN")]
        [Required]
        public DateTime FechaFin { get; set; }

        [Column("ACTIVO_EVEN")]
        [Required]
        public bool Activo { get; set; }

        [Column("UBICACION_EVEN")]
        [Required]
        public int MunicipioId { get; set; }

        public Municipio Municipio { get; set; }

        public List<Subasta> Subastas { get; set; } = new List<Subasta>();
    }
}
