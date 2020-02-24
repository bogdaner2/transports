using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using Transports.Core.Interfaces;
using Transports.Core.Models.SQL;
using Transports.Core.Repositories;

namespace Transports.WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class DriversController : ApiController
    {
        private readonly IRepository<Driver> _driversRepo = new ContextRepository<Driver>();

        // GET api/values
        public IEnumerable<Driver> Get()
        {
            return _driversRepo.GetAll();
        }


        [HttpPost]
        public void Post(Driver data)
        {
            _driversRepo.Create(data);
        }

        // PUT api/values/5
        public void Put(Driver data)
        {
            var item = _driversRepo.Get(x => x.DriverId == data.DriverId);

            item.Age = data.Age;
            item.Rang = data.Rang;
            item.Name = data.Name;

            _driversRepo.Update(item);
        }

        public void Delete(string id)
        {
            var Guid = System.Guid.Parse(id);

            var item = _driversRepo.Get(x => x.DriverId == Guid);

            _driversRepo.Remove(item);
        }
    }
}