using Subasta.core.dtos;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.interfaces
{
    public interface IMunicipioService : IGenericCrudService<MunicipioDto, Municipio>
    {
        List<MunicipioDto> GetByDepartamentoId(int id);
    }
}
