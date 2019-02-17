using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using Transports.Core.Models;

namespace Transports.Core
{
    public class ContextRepository<T> where  T: class, IEntity
    {
        private readonly Table<T> _table;
        private readonly TransportsContext _context;
        public ContextRepository()
        {
            _context = new TransportsContext();
            _table = _context.GetTable<T>();
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
                _context.SubmitChanges();
                return item;
            }
            catch
            {
                return null;
            }
        }
        public void Save()
        {
            _context.SubmitChanges();
        }
        public bool Delete(T item)
        {
            try
            {
                _table.DeleteOnSubmit(item);
                _context.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
