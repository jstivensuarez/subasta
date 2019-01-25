using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class TipoDocumentoRepository: GenericRepository<TipoDocumento>, ITipoDocumentoRepository
    {
        readonly SubastaContext context;

        public TipoDocumentoRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }
    }
}
