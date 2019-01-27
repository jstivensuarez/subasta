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
        public string Nombre { get; set; }

        [Column("DESCRIPCION_ROL")]
        public string Descripcion { get; set; }

        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
