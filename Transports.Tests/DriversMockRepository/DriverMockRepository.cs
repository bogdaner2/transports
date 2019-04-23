using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Transports.Core.Interfaces;
using Transports.Core.Models.SQL;

namespace Transports.Tests.DriversMockRepository
{
    public class DriverMockRepository: IRepository<Driver>
    {
        private List<Driver> _drivers = new List<Driver>();

        public IEnumerable<Driver> GetAll()
        {
            return _drivers;
        }

        public Driver Get(Expression<Func<Driver, bool>> predicate)
        {
            return _drivers.FirstOrDefault(predicate.Compile());
        }

        public Driver Create(Driver item)
        {
            _drivers.Add(item);
            return item;
        }

        public bool Update(Driver item)
        {
            try
            {
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Remove(Driver driver)
        {
            try
            {
                _drivers.Remove(driver);
                return true;
            }
            catch (Exception e)
            {
                return false;   
            }
        }

        public void Clear()
        {
            _drivers = new List<Driver>();
        }
    }
}
