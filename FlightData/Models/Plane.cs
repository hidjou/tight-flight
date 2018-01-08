using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightData.Models
{
    public class Plane
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public int MaxCapacity { get; set; }
        // 1- Passenger, 2- Cargo
        public int Type { get; set; }

        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }
        public int FlyingHours { get; set; }                    // Set 'defaultValue' to 0 in migration
        // 0- Free, 1- Flying, 2- Under maintenance
        public int Status { get; set; }

    }
}
