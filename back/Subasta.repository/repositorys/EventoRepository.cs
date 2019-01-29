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
    public class EventoRepository: GenericRepository<Evento>, IEventoRepository
    {
        readonly SubastaContext context;

        public EventoRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }

        public Evento AddWithReturn(Evento entity)
        {
            try
            {
                context.Eventos.Add(entity);
                context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al agregar la entidad", ex);
            }
        }

        public Evento GetWithAll(object id)
        {
            try
            {
                var entity = context.Eventos.AsNoTracking()
                    .Include(c => c.Municipio)
                    .SingleOrDefault(c => c.EventoId == Convert.ToInt32(id));
                return entity;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al buscar la entidad", ex);
            }
        }

        public List<Evento> GetllWithInclude()
        {
            try
            {
                var entitys = context.Eventos.AsNoTracking()
                    .Include(c => c.Municipio)
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
