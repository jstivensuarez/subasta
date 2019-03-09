using Subasta.core.dtos.reportes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.interfaces
{
    public interface IReporteService
    {
        List<LotesVendidos> GetLotesVendidos(int eventoId);

        List<CompradorLote> GetCompradoresPorLote(int loteId);
    }
}
