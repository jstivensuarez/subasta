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
        [MaxLength(50)]
        [Required]
        public string Nombre { get; set; }

        [Column("DESCRIPCION_LOTE")]
        [MaxLength(2000)]
        public string Descripcion { get; set; }

        [Column("CANTIDAD_ELEMENTOS_LOTE")]
        [Required]
        public int CantidadElementos { get; set; }

        [Column("PESO_PROMEDIO_LOTE")]
        [Required]
        public decimal PesoPromedio { get; set; }

        [Column("PESO_TOTAL_LOTE")]
        [Required]
        public decimal PesoTotal { get; set; }

        [Column("PRECIO_BASE_LOTE")]
        [Required]
        public decimal PrecioBase { get; set; }

        [Column("FOTO_LOTE")]
        [MaxLength(200)]
        [Required]
        public string FotoLote { get; set; }

        [Column("ACTIVO_LOTE")]
        [Required]
        public bool Activo { get; set; }

        [Column("FINALIZADO_LOTE")]
        [Required]
        public bool Finalizado { get; set; }

        [Column("ID_CLIENTE_LOTE")]
        [Required]
        public string ClienteId { get; set; }

        public Cliente Cliente { get; set; }

        [Column("COD_MUN_UBI_LOTE")]
        [Required]
        public int MunicipioId { get; set; }

        public Municipio Municipio { get; set; }

        [Column("COD_SUBASTA_LOTE")]
        [Required]
        public int SubastaId { get; set; }

        [Column("PRECIO_INICIAL_LOTE")]
        [Required]
        public decimal PrecioInicial { get; set; }

        public Subasta Subasta { get; set; }

        [Column("COD_CATEGORIA_LOTE")]
        [Required]
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }


        [Column("COD_RAZA_LOTE")]
        [Required]
        public int RazaId { get; set; }

        public Raza Raza { get; set; }
        
        [Column("COD_CLASIFICACION_LOTE")]
        [Required]
        public int ClasificacionId { get; set; }

        public Clasificacion Clasificacion { get; set; }

        public List<Animal> Animales { get; set; } = new List<Animal>();

        public List<Pujador> Pujadores { get; set; } = new List<Pujador>();

        public List<ConfirmacionPago> confirmaciones { get; set; } = new List<ConfirmacionPago>();
    }
}