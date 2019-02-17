using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subasta.repository.models
{
    [Table("TBL_CLIENTES")]
    public class Cliente
    {
        [Key]
        [Column("ID_CLI")]
        [MaxLength(15)]
        public string ClienteId { get; set; }

        [Column("NOMBRE_CLI")]
        [MaxLength(70)]
        [Required]
        public string Nombre { get; set; }

        [Column("CORREO_CLI")]
        [MaxLength(50)]
        [Required]
        public string Correo { get; set; }

        [Column("TELEFONO_CLI")]
        [MaxLength(20)]
        [Required]
        public string Telefono { get; set; }

        [Column("DIRECCION_CLI")]
        [MaxLength(50)]
        [Required]
        public string Direccion { get; set; }

        [Column("REPRESENTANTE_LEGAL_CLI")]
        [MaxLength(70)]
        public string Representante { get; set; }

        [Column("USUARIO_CLI")]
        [MaxLength(50)]
        public string Usuario { get; set; }

        [Column("TIPO_CLI")]
        [MaxLength(20)]
        [Required]
        public string Tipo { get; set; }

        [Column("ACTIVO_CLI")]
        [Required]
        public bool Activo { get; set; }

        [Column("CODIGO_TD_CLI")]
        [Required]
        public int TipoDocumentoId { get; set; }

        public TipoDocumento TipoDocumento { get; set; }

        [Column("CODIGO_MUN_CLI")]
        [Required]
        public int MunicipioId { get; set; }

        public Municipio Municipio { get; set; }

        public List<Pujador> Pujadores { get; set; } = new List<Pujador>();

        public List<Lote> Lotes { get; set; } = new List<Lote>();

        public List<SolicitudSubasta> SolicitudSubastas { get; set; } = new List<SolicitudSubasta>();
    }
}
