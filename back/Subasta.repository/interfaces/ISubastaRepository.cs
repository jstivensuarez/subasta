using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.interfaces
{
    public interface ISubastaRepository : IGenericRepository<models.Subasta>
    {
        List<models.Subasta> GetAllWithInclude();
        List<Subasta.repository.models.Subasta> GetPorEvento(object id);

        void LogicDelete(int id);
    }
}
