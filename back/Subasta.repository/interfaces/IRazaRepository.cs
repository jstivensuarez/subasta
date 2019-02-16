using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.interfaces
{
    public interface IRazaRepository: IGenericRepository<Raza>
    {
        int Add(Raza entity);
        List<Raza> GetllWithInclude();
    }
}
