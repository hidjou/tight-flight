﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TightFlight.Models.Flight
{
    public class FlightSearchResults
    {
        public string From { get; set; }
        public string To { get; set; }

        public IEnumerable<FlightSearchResults> Flights { get; set; }
        public IEnumerable<string> SearchResults { get; set; }
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int Duration { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string Plane { get; set; }
        public string Status { get; set; }
    }
}