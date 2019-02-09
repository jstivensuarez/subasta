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
    public class LoteRepository: GenericRepository<Lote>, ILoteRepository
    {
        readonly SubastaContext context;

        public LoteRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }

        public List<Lote> GetAllWithInclude()
        {
            try
            {
                var entitys = context.Lotes.AsNoTracking()
                    .Include(c => c.Municipio)
                    .Include(c => c.Subasta)
                    .Include(c => c.Cliente)
                    .Where(a => a.Activo)
                    .ToList();
                return entitys;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al buscar la entidad", ex);
            }
        }

        public List<Lote> GetAllNoAssociate(string clienteId)
        {
            try
            {
                var entitys = (from lotes in context.Lotes
                               where (from p in context.Pujadores
                                      where p.LoteId == lotes.LoteId
                                      && p.ClienteId == clienteId
                                      select p).Count() == 0
                                      
                        select lotes).ToList();
                return entitys;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al buscar la entidad", ex);
            }
        }
    }
}
