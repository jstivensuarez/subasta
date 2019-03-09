using Subasta.core.dtos;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.interfaces
{
    public interface IEventoService: IGenericCrudService<EventoDto, Evento>
    {
        EventoDto AddWithReturn(EventoDto dto);

        List<EventoDto> GetForClients();

        List<EventoDto> GetForClientAutenticated(string usuario);

        List<EventoDto> GetAllWithOutInclude();
    }
}
