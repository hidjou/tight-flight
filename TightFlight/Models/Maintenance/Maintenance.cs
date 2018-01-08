using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TightFlight.Models.Maintenance
{
    public class Maintenance
    {
        public string EmployeeName { get; set; }
        public DateTime Date { get; set; }
        public string MaintenanceType { get; set; }
        public string PlaneModel { get; set; }
        public string PlaneType { get; set; }
    }
}
