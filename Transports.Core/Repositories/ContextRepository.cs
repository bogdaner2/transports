using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using Transports.Core.Contexts;
using Transports.Core.Interfaces;
using Transports.Core.Models;

namespace Transports.Core.Repositories
{
    public class ContextRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DbSet<T> _table;
        public ContextRepository()
        {
            _table = TransportsContext.Instance.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _table.FirstOrDefault(predicate);
        }

        public T Create(T item)
        {
            try
            {
                _table.Add(item);
                Save();
                return item;
            }
            catch
            {
                return null;
            }
        }
        public async void Save()
        {
            await TransportsContext.Instance.SaveChangesAsync();
        }
        public bool Remove(T item)
        {
            try
            {
                _table.Remove(item);
                Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
