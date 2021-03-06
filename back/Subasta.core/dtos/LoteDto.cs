﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Subasta.core.dtos
{
    public class LoteDto
    {
        public int LoteId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int CantidadElementos { get; set; }

        public decimal PesoTotal { get; set; }

        public decimal PrecioBase { get; set; }

        public string FotoLote { get; set; }

        public decimal PrecioInicial { get; set; }

        public decimal ValorAnticipo { get; set; }

        public string ClienteId { get; set; }

        public ClienteDto Cliente { get; set; }

        public int MunicipioId { get; set; }

        public MunicipioDto Municipio { get; set; }

        public int SubastaId { get; set; }

        public SubastaDto Subasta { get; set; }

        public IFormFile Imagen { get; set; }
    }
}