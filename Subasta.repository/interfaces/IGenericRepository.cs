using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Subasta.repository.interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();

        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        T Find(object id);

        void Add(T entity);

        void Delete(T entity);

        void Edit(T entity);
    }
}
