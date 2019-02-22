using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using Transports.Core.Contexts;
using Transports.Core.Models;

namespace Transports.Core
{
    public class InMemoryRepository<T> where T : class, IEntity
    {
        private readonly Table<T> _table;
        public InMemoryRepository()
        {
            _table = TransportsContext.Instance.GetTable<T>();
        }

        public List<T> GetAll()
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
                _table.InsertOnSubmit(item);
                Save();
                return item;
            }
            catch
            {
                return null;
            }
        }
        public void Save()
        {
            TransportsContext.Instance.SubmitChanges();
        }
        public bool Delete(T item)
        {
            try
            {
                _table.DeleteOnSubmit(item);
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
