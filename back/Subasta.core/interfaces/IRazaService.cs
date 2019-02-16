using Subasta.core.dtos;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.interfaces
{
    public interface IRazaService : IGenericCrudService<RazaDto, Raza>
    {
        int AddWithReturn(RazaDto dto);

        List<RazaDto> GetByCategoria(int id);
    }
}
