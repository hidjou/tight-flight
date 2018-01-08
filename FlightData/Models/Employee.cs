using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightData.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        // 1- Flight-deck crew, 2- Cabin crew, 3- Ground crew
        public int Job { get; set; }
        [Required][StringLength(5)]
        public string EmployeeNumber { get; set; }
        public int TotalHours { get; set; }                     // Set 'defaultValue' to 0 in migration
        // 0- Free, 1- Busy
        public int Status { get; set; }                         // Set 'defaultValue' to 0 in migration
    }
}
