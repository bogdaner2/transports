using Transports.Core.Interfaces;
using Transports.Core.Models.SQL;
using Transports.Core.Repositories;

namespace Transports.Core.DependencyInjection
{
    public static class ServiceLocator
    {
        public static IRepository<Driver> DriversRepo { get; set; }
        public static ContextRepository<Shift> _shiftsRepo = new ContextRepository<Shift>();
        public static ContextRepository<Route> _routesRepo = new ContextRepository<Route>();

        public static void SetDriverRepo(IRepository<Driver> driverRepo)
        {
            DriversRepo = driverRepo;
        }
    }
}
