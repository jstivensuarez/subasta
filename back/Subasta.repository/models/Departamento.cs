using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subasta.repository.models
{
    [Table("TBL_DEPARTAMENTOS")]
    public class Departamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_DPTO")]
        public int DepartamentoId { get; set; }

        [Column("NOMBRE_DPTO")]
        public string Descripcion { get; set; }

        public List<Municipio> Municipios { get; set; } = new List<Municipio>();
    }
}
