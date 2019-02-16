using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.interfaces
{
    public interface IClasificacionRepository : IGenericRepository<Clasificacion>
    {
        int Add(Clasificacion entity);
        
        List<Clasificacion> GetllWithInclude();
    }
}
