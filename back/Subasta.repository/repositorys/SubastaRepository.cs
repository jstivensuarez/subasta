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

        public List<models.Subasta> GetAllWithInclude()
        {
            try
            {
                var entitys = context.Subastas.AsNoTracking()
                    .Where(c => c.Activo)
                    .ToList();
                return entitys;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al buscar la entidad", ex);
            }
        }

        public List<models.Subasta> GetPorEvento(object id)
        {
            try
            {
                var entitys = context.Subastas.
                    Where(s => s.EventoId == Convert.ToInt32(id) && s.Activo).ToList();
                return entitys;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al buscar las entidades", ex);
            }
        }

        public void LogicDelete(int id)
        {
            try
            {
                var entity = context.Subastas.Find(id);
                entity.Activo = false;
                context.Subastas.Update(entity);
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al eliminar la entidad", ex);
            }
        }
    }
}
