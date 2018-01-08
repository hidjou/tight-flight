using System;
using System.Collections.Generic;
using System.Text;

namespace FlightData.Models
{
    public class FlightDistance
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int Distance { get; set; }
    }
}
