using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TightFlight.Models.Plane
{
    public class PlaneFormModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int MaxCapacity { get; set; }
        public string Identifier { get; set; }
        public DateTime Year { get; set; }
        public int FlyingHours { get; set; }
    }
}
