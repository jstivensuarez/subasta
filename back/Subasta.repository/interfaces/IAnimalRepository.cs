using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.interfaces
{
    public interface IAnimalRepository : IGenericRepository<Animal>
    {
        List<Animal> GetllWithInclude();
    }
}
