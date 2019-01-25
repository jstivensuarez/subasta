using Subasta.core.dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.interfaces
{
    public interface ISubastaService : IGenericCrudService<SubastaDto, repository.models.Subasta>
    {
    }
}
