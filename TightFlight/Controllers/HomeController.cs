using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TightFlight.Models;
using TightFlight.Models.Home;
using FlightData;
using System.Linq;

namespace TightFlight.Controllers
{
    public class HomeController : Controller
    {

        private IFlight _flights;

         public HomeController(IFlight Flights)
         {
             _flights = Flights;
         }

    public IActionResult Index()
        {
            var searchResults = _flights.GetCities();

            var dataListResult = searchResults
               .Select(result => new SearchModel
                {
                City = result
                });
            
            var model = new SearchModelIndex()
            {
                Cities = dataListResult
            };
            
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
