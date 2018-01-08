using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightData.Models
{
    public class MaintenanceType
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
