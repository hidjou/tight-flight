using FlightData;
using FlightData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using TightFlight.Models.Plane;
using System;

namespace TightFlight.Controllers
{
    public class PlaneController : Controller
    {
        private IPlane _planes;
        private SignInManager<AuthUser> _signManager;
        private UserManager<AuthUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;

        public PlaneController(IPlane Planes, UserManager<AuthUser> userManager, SignInManager<AuthUser> signManager, RoleManager<ApplicationRole> roleManager)
        {
            _planes = Planes;
            _userManager = userManager;
            _signManager = signManager;
            _roleManager = roleManager;
        }

        // CREATE
        public IActionResult Create()
        {
            if (User.IsInRole("Admin") == false) {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(Plane newPlane)
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (newPlane == null)
            {
                throw new ArgumentNullException(nameof(newPlane));
            }

            var _newPlane = new Plane
            {
                MaxCapacity = newPlane.MaxCapacity,
                Type = newPlane.Type,
                Manufacturer = newPlane.Manufacturer,
                Model = newPlane.Model,
                Year = newPlane.Year
            };
            _planes.Add(_newPlane);
            return RedirectToAction("Index", "Plane");
        }

        // READ
        public IActionResult Index()
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var planeModels = _planes.GetAll();

            var listingResult = planeModels
                .Select(result => new PlaneListingModel
                {
                    Id = result.Id,
                    MaxCapacity = _planes.GetMaxCapacity(result.Id),
                    Type = (_planes.GetType(result.Id) == 1) ? "Passenger plane" : "Cargo plane",
                    Manufacturer = _planes.GetManufacturer(result.Id),
                    Model = _planes.GetModel(result.Id),
                    // Take only the year and cast to string
                    Year = _planes.GetYear(result.Id).Year.ToString(),
                    FlyingHours = _planes.GetFlyingHours(result.Id),
                    Identifier = _planes.GetIdentifier(result.Id),
                    Status = _planes.GetStatus(result.Id)
                });

            var model = new PlaneIndexModel()
            {
                Planes = listingResult
            };

            return View(model);
        }

        // UPDATE
        public IActionResult Edit(int id)
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var planeModel = _planes.GetById(id);

            var formData = new PlaneFormModel
            {
                Id = id,
                MaxCapacity = planeModel.MaxCapacity,
                Identifier = planeModel.Identifier,
                Type = planeModel.Type,
                Manufacturer = planeModel.Manufacturer,
                Model = planeModel.Model,
                Year = planeModel.Year,
                FlyingHours = planeModel.FlyingHours
            };

            return View(formData);
        }

        public IActionResult Update(PlaneFormModel planeToBeUpdated)
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            Plane _plane = _planes.GetById(planeToBeUpdated.Id);
            Plane newPlane = new Plane
            {
                Id = planeToBeUpdated.Id,
                Identifier = planeToBeUpdated.Identifier,
                MaxCapacity = planeToBeUpdated.MaxCapacity,
                Type = planeToBeUpdated.Type,
                Manufacturer = planeToBeUpdated.Manufacturer,
                Model = planeToBeUpdated.Model,
                Year = planeToBeUpdated.Year,
                FlyingHours = planeToBeUpdated.FlyingHours,
            };

            _planes.Update(_plane.Id, newPlane);
            return RedirectToAction("Index", "Plane");
        }

        // DELETE
        public IActionResult Remove(int id)
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            _planes.Delete(id);

            return RedirectToAction("Index", "Plane");
        }
    }
}
