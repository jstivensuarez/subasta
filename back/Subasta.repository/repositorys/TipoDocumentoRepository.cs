using Subasta.repository.exceptions;
using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class TipoDocumentoRepository : GenericRepository<TipoDocumento>, ITipoDocumentoRepository
    {
        readonly SubastaContext context;

        public TipoDocumentoRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }

        public int Add(TipoDocumento entity)
        {
            try
            {
                context.TipoDocumentos.Add(entity);
                context.SaveChanges();
                return entity.TipoDocumentoId;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al agregar la entidad", ex);
            }
        }
    }
}
