using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlightData.Models
{
    public class FlightEmployee
    {
        public int Id { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
