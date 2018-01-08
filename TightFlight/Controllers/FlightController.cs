using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FlightData.Models;
using FlightData;
using TightFlight.Models.Flight;
using System.Diagnostics;
using System;
using System.Globalization;
using System.Collections.Generic;

namespace TightFlight.Controllers
{
    public class FlightController : Controller
    {
        private IFlight _flights;

        public FlightController(IFlight Flights)
        {
            _flights = Flights;
        }
        public IActionResult Index()
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var flightModels = _flights.GetAll().OrderByDescending(flight => flight.Departure);

            var listingResult = flightModels
                .Select(result => new FlightListingModel
                {
                    Id = result.Id,
                    Origin = _flights.GetOrigin(result.Id),
                    Destination = _flights.GetDestination(result.Id),
                    Departure = _flights.GetDeparture(result.Id).ToString(),
                    Duration = _flights.GetDuration(result.Id),
                    Arrival = _flights.GetDeparture(result.Id).AddSeconds(_flights.GetDuration(result.Id) * Config.Scale).ToString(),
                    Plane = _flights.GetById(result.Id).Plane.Model,
                    Status = _flights.GetStatus(result.Id)
                });

            var model = new FlightIndexModel()
            {
                Flights = listingResult
            };

            return View(model);
        }
        public IActionResult Create()
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(Flight NewFlight)
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (NewFlight == null)
            {
                throw new ArgumentNullException(nameof(NewFlight));
            }

            var newFlight = new Flight
            {
                Origin = NewFlight.Origin,
                Destination = NewFlight.Destination,
                Departure = NewFlight.Departure,
                Status = 0,
                PlaneId = NewFlight.PlaneId
            };

            _flights.Add(newFlight);
            return RedirectToAction("Index", "Flight");
        }
        public IActionResult SearchResults(string From, string To)
        {
            IEnumerable<Flight> flightModels;
            if (User.IsInRole("Admin"))
            {
                flightModels = _flights.GetAll().Where(s => s.Origin.ToLower().Equals(From.ToLower()) && s.Destination.ToLower().Equals(To.ToLower())).OrderByDescending(flight => flight.Departure);
            }
            else {
                flightModels = _flights.GetAll().Where(s => s.Status == 0 && s.Origin.ToLower().Equals(From.ToLower()) && s.Destination.ToLower().Equals(To.ToLower())).OrderByDescending(flight => flight.Departure);
            }

            var listingResult = flightModels
                .Select(result => new FlightSearchResults
                {
                    Id = result.Id,
                    Origin = _flights.GetOrigin(result.Id),
                    Destination = _flights.GetDestination(result.Id),
                    Departure = _flights.GetDeparture(result.Id).ToString(),
                    Duration = _flights.GetDuration(result.Id),
                    Arrival = _flights.GetDeparture(result.Id).AddSeconds(_flights.GetDuration(result.Id) * Config.Scale).ToString(),
                    Plane = _flights.GetById(result.Id).Plane.Model,
                    Status = _flights.GetStatus(result.Id)
                });

            var model = new FlightSearchResults()
            {
                Flights = listingResult
            };

            return View(model);
        }
        public IActionResult ViewResult(int Id) {
            if(Id <= 0)
                return RedirectToAction("Index", "Home");

            if (_flights.GetById(Id).Status != 0 && User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }

            var result = _flights.GetById(Id);

            var model = new FlightViewResult
            {
                Id = result.Id,
                Origin = _flights.GetOrigin(result.Id),
                Destination = _flights.GetDestination(result.Id),
                Departure = _flights.GetDeparture(result.Id).ToString(),
                Duration = _flights.GetDuration(result.Id),
                Arrival = _flights.GetDeparture(result.Id).AddSeconds(_flights.GetDuration(result.Id) * Config.Scale).ToString(),
                Plane = _flights.GetById(result.Id).Plane.Manufacturer + " " + _flights.GetById(result.Id).Plane.Model,
                Status = _flights.GetStatus(result.Id)
            };

            return View(model);
        }
  	public IActionResult Booking()
        {
            return View();
        }

        public IActionResult Done()
        {
            return View();
        }
    }
}
