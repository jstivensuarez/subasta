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
    public class RazaRepository : GenericRepository<Raza>, IRazaRepository
    {
        readonly SubastaContext context;

        public RazaRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }

        public int Add(Raza entity)
        {
            try
            {
                context.Razas.Add(entity);
                context.SaveChanges();
                return entity.RazaId;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al agregar la entidad", ex);
            }
        }

        public List<Raza> GetllWithInclude()
        {
            try
            {
                var entitys = context.Razas.AsNoTracking()
                    .Include(c => c.Categoria)
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
