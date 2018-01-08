using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TightFlight.Models.Plane
{
    public class PlaneIndexModel
    {
        public IEnumerable<PlaneListingModel> Planes { get; set; }
    }
}
