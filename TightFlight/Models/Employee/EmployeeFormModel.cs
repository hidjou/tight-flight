using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TightFlight.Models.Employee
{
    public class EmployeeFormModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Job { get; set; }
        public string EmployeeNumber { get; set; }
        public int TotalHours { get; set; }
    }
}
