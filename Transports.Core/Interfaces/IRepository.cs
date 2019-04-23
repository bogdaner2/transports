using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Transports.Core.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> predicate);
        T Create(T item);
        bool Update(T item);
        bool Remove(T item);
    }
}
