using Subasta.core.dtos;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.interfaces
{
    public interface ISolicitudService: IGenericCrudService<SolicitudSubastaDto, SolicitudSubasta>
    {
        void Add(SolicitudSubastaDto dto, string usuario);

        void Aceptar(SolicitudSubastaDto entity);

        List<SolicitudSubastaDto> GetAll(string estado);
    }
}
