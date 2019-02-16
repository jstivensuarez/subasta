﻿using Subasta.core.dtos;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.interfaces
{
    public interface ITipoDocumentoService : IGenericCrudService<TipoDocumentoDto, TipoDocumento>
    {
        int AddWithReturn(TipoDocumentoDto dto);
    }
}
