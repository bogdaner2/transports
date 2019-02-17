using System.Collections.Generic;
using Transports.Core.Models;

namespace Transports.Core
{
    public static class InMemoryContext
    {
        public static List<Driver> Drivers = new List<Driver>();
        public static List<Shift> Shifts = new List<Shift>();
        public static List<DriverShift> DriverShifts = new List<DriverShift>();
        public static List<Transport> Transports = new List<Transport>();
        public static List<Route> Routes = new List<Route>();
    }
}
