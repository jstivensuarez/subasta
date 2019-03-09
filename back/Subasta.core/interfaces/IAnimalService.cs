using Subasta.core.dtos;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.interfaces
{
    public interface IAnimalService: IGenericCrudService<AnimalDto, Animal>
    {
        List<AnimalDto> GetAllByLote(int loteId);

        List<AnimalDto> GetAllWithOutInclude();
    }
}
