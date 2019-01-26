using System.Collections.Generic;

namespace Subasta.core.dtos
{
    public class ClienteDto
    {

        public string ClienteId { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public string Representante { get; set; }

        public string Usuario { get; set; }

        public string Estado { get; set; }

        public int TipoDocumentoId { get; set; }

        public TipoDocumentoDto TipoDocumento { get; set; }

        public int MunicipioId { get; set; }

        public MunicipioDto Municipio { get; set; }

        public List<PujadorDto> Pujadores { get; set; } = new List<PujadorDto>();

        public List<LoteDto> Lotes { get; set; } = new List<LoteDto>();
    }
}
