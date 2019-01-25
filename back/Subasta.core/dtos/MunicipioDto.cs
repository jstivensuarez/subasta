using System.Collections.Generic;

namespace Subasta.core.dtos
{
    public class MunicipioDto
    {
        public int MunicipioId { get; set; }

        public string Descripcion { get; set; }

        public int DepartamentoId { get; set; }

        public DepartamentoDto Departamento { get; set; }

        public List<EventoDto> Eventos { get; set; } = new List<EventoDto>();

        public List<ClienteDto> Clientes { get; set; } = new List<ClienteDto>();

        public List<LoteDto> Lotes { get; set; } = new List<LoteDto>();

        public List<AnimalDto> Animales { get; set; } = new List<AnimalDto>();
    }
}