using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subasta.repository.models
{
    [Table("TBL_LOTES")]
    public class Lote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODIGO_LOTE")]
        public int LoteId { get; set; }

        [Column("NOMBRE_LOTE")]
        public string Nombre { get; set; }

        [Column("DESCRIPCION_LOTE")]
        public string Descripcion { get; set; }

        [Column("CANTIDAD_ELEMENTOS_LOTE")]
        public int CantidadElementos { get; set; }

        [Column("PESO_PROMEDIO_LOTE")]
        public decimal PesoPromedio { get; set; }

        [Column("PESO_TOTAL_LOTE")]
        public decimal PesoTotal { get; set; }

        [Column("PRECIO_BASE_LOTE")]
        public decimal PrecioBase { get; set; }

        [Column("FOTO_LOTE")]
        public string FotoLote { get; set; }

        [Column("VALOR_ANTICIPO_LOTE")]
        public decimal ValorAnticipo { get; set; }

        [Column("ID_CLIENTE_LOTE")]
        public string ClienteId { get; set; }

        public Cliente Cliente { get; set; }

        [Column("COD_MUN_UBI_LOTE")]
        public int MunicipioId { get; set; }

        public Municipio Municipio { get; set; }

        [Column("COD_SUBASTA_LOTE")]
        public int SubastaId { get; set; }

        [Column("PRECIO_INICIAL_LOTE")]
        public decimal PrecioInicial { get; set; }

        public Subasta Subasta { get; set; }

        public List<Animal> Animales { get; set; } = new List<Animal>();

        public List<Pujador> Pujadores { get; set; } = new List<Pujador>();
    }
}