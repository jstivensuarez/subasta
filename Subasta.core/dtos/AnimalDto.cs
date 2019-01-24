namespace Subasta.core.dtos
{
    public class AnimalDto
    {

        public int AnimalId { get; set; }

        public string Foto { get; set; }

        public decimal Peso { get; set; }

        public string Descripcion { get; set; }

        public int CategoriaId { get; set; }

        public CategoriaDto Categoria { get; set; }

        public int RazaId { get; set; }

        public RazaDto Raza { get; set; }

        public int SexoId { get; set; }

        public SexoDto Sexo { get; set; }

        public int MunicipioId { get; set; }

        public MunicipioDto Municipio { get; set; }

        public int LoteId { get; set; }

        public LoteDto Lote { get; set; }
    }
}
