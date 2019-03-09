using Microsoft.EntityFrameworkCore;
using Subasta.repository.exceptions;
using Subasta.repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Subasta.repository.repositorys
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        internal SubastaContext context;

        public GenericRepository(SubastaContext context)
        {
            this.context = context;
        }

        public void Add(T entity)
        {
            try
            {
                context.Set<T>().Add(entity);
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al agregar la entidad", ex);
            }

        }

        public void Delete(T entity)
        {
            try
            {
                context.Set<T>().Remove(entity);
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al eliminar la entidad", ex);
            }
        }

        public void Edit(T entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al editar la entidad", ex);
            }

        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                IQueryable<T> query = context.Set<T>().Where(predicate);
                context.Entry(query).State = EntityState.Detached;
                return query;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al buscar la entidad", ex);
            }

        }

        public T Find(object id)
        {
            try
            {
                var entity = context.Set<T>().Find(id);
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al buscar la entidad", ex);
            }

        }

        public virtual List<T> GetAll()
        {
            try
            {
                List<T> query = context.Set<T>().ToList();
                foreach (var item in query)
                {
                    context.Entry(item).State = EntityState.Detached;
                }

                return query;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al obetener las entidades", ex);
            }

        }
    }
}
