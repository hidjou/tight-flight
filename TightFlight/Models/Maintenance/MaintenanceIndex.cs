using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TightFlight.Models.Maintenance
{
    public class MaintenanceIndex
    {
        public IEnumerable<Maintenance> Maintenances { get; set; }
    }
}
