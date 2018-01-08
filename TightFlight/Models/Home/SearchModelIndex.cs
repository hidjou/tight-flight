using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TightFlight.Models.Home
{
    public class SearchModelIndex
    {
        public string From { get; set; }
        public string To { get; set; }
        public IEnumerable<SearchModel> Cities { get; set; }
    }
}