using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subasta.repository.models
{
    [Table("TBL_MUNICIPIOS")]
    public class Municipio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_MUN")]
        public int MunicipioId { get; set; }

        [Column("NOMBRE_MUN")]
        public string Descripcion { get; set; }

        [Column("COD_DPTO_MUN")]
        public int DepartamentoId { get; set; }

        public Departamento Departamento { get; set; }

        public List<Evento> Eventos { get; set; } = new List<Evento>();

        public List<Cliente> Clientes { get; set; } = new List<Cliente>();

        public List<Lote> Lotes { get; set; } = new List<Lote>();

        public List<Animal> Animales { get; set; } = new List<Animal>();
    }
}
