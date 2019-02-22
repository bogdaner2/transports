using System;
using System.Collections.Generic;
using Transports.Core.Interfaces;
using Transports.Core.Models;

namespace Transports.Core.Contexts
{
    public class InMemoryContext : IContext
    {
        private static readonly Lazy<InMemoryContext> Lazy = new Lazy<InMemoryContext>(() => new InMemoryContext());
        public static InMemoryContext Instance => Lazy.Value;

        private InMemoryContext() { }

        public List<Driver> Drivers = new List<Driver>();
        public List<Shift> Shifts = new List<Shift>();
        public List<DriverShift> DriverShifts = new List<DriverShift>();
        public List<Transport> Transports = new List<Transport>();
        public List<Route> Routes = new List<Route>();
    }
}
