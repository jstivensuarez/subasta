using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.interfaces
{
    public interface ILoteRepository: IGenericRepository<Lote>
    {
        List<Lote> GetllWithInclude();

        void LogicDelete(int id);
    }
}
