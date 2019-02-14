using Microsoft.EntityFrameworkCore;
using Subasta.repository.exceptions;
using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class SolicitudRepository : GenericRepository<SolicitudSubasta>, ISolicitudRepository
    {
        readonly SubastaContext context;

        public SolicitudRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }

        public List<SolicitudSubasta> GetllWithInclude()
        {
            try
            {
                var entitys = context.SolicitudSubastas.AsNoTracking()
                    .Include(c => c.Cliente)
                    .Include(c => c.Subasta)
                    .ToList();
                return entitys;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al buscar la entidad", ex);
            }
        }
    }
}
