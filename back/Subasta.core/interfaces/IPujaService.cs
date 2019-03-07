using Subasta.core.dtos;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.interfaces
{
    public interface IPujaService: IGenericCrudService<PujaDto,Puja>
    {
        List<PujaDto> obtenerGanadores();

        void NotificarGanadores(List<PujaDto> ganadores);

        PujaDto obtenerGanadorInfo(int loteId);

        void ConfirmarGanador(int loteId, string usuario);

        void ActualizarConfirmaciones();

        void ActualizarLotesHuerfanos();
    }
}
