using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TightFlight.Models.Employee
{
    public class EmployeeIndexModel
    {
        public IEnumerable<EmployeeListingModel> Employees { get; set; }
    }
}
