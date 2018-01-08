using System.Collections.Generic;

namespace TightFlight.Models.Flight
{
    public class FlightIndexModel
    {
        public IEnumerable<FlightListingModel> Flights { get; set; }
    }
}
