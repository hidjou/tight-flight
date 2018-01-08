using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TightFlight.Models.Plane
{
    // Plane data transformation model (view model)
    public class PlaneListingModel
    {
        public int Id { get; set; }
        public int MaxCapacity { get; set; }
        public string Type { get; set; }
        public string Identifier { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Status { get; set; }
        public int FlyingHours { get; set; }
    }
}
