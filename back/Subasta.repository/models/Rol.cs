using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subasta.repository.models
{
    [Table("TBL_ROLES")]
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_ROL")]
        public int RolId { get; set; }

        [Column("NOMBRE_ROL")]
        [MaxLength(50)]
        [Required]
        public string Nombre { get; set; }

        [Column("DESCRIPCION_ROL")]
        [MaxLength(50)]
        public string Descripcion { get; set; }

        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
