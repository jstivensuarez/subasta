using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.interfaces
{
    public interface IClienteRepository: IGenericRepository<Cliente>
    {
        List<Cliente> GetAllWithInclude();
        Cliente GetWithAll(object id);
        void LogicDelete(string id);
    }
}
