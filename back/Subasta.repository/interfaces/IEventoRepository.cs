using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.interfaces
{
    public interface IEventoRepository : IGenericRepository<Evento>
    {
        Evento AddWithReturn(Evento entity);

        Evento GetWithAll(object id);

        List<Evento> GetAllWithInclude();
    }
}
