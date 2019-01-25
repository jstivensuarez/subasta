using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Subasta.core.interfaces
{
    public interface IGenericCrudService<T,S>
    {
        void Add(T dto);
        void Delete(T entity);
        void Edit(T entity);
        IQueryable<T> Find(Expression<Func<S, bool>> predicate);
        T Find(object id);
        List<T> GetAll();
    }
}
