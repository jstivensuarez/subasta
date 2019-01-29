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
    public class SubastaRepository: GenericRepository<models.Subasta>, ISubastaRepository
    {
        readonly SubastaContext context;

        public SubastaRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }

        public List<models.Subasta> GetPorEvento(object id)
        {
            try
            {
                var entitys = context.Subastas.
                    Where(s => s.EventoId == Convert.ToInt32(id)).ToList();
                return entitys;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al buscar las entidades", ex);
            }
        }
    }
}
