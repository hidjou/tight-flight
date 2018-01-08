using System;
using System.Collections.Generic;
using System.Text;

namespace FlightData.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime Departure { get; set; }
        public int Duration { get; set; }
        public int Status { get; set; } = 0;    // 0 = yet to depart, 1 = under way, 2 = completed

        public int PlaneId { get; set; }
        public virtual Plane Plane { get; set; }
    }
}
