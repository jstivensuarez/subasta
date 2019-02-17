using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subasta.repository.models
{
    [Table("TBL_USUARIOS")]
    public class Usuario
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_USU")]
        public int UsuarioId { get; set; }

        [Column("NOMBRE_USUARIO")]
        [MaxLength(50)]
        [Required]
        public string Nombre { get; set; }

        [Column("CORREO_USUARIO")]
        [MaxLength(50)]
        [Required]
        public string Correo { get; set; }

        [Column("PASS_USUARIO")]
        [MaxLength(50)]
        [Required]
        public string Clave { get; set; }

        [Column("ROL_USUARIO")]
        public int RolId { get; set; }

        public Rol Rol { get; set; }

    }
}
