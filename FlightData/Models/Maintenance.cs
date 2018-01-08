using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlightData.Models
{
    public class Maintenance
    {
        public int Id { get; set; }

        public int PlaneId { get; set; }
        public Plane Plane { get; set; }
        
        public int MaintenanceTypeId { get; set; }
        public MaintenanceType MaintenanceType { get; set; }

        public DateTime Date { get; set; }
    }
}
