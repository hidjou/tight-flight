using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightData;
using FlightData.Models;
using System.Diagnostics;
using TightFlight.Models.Maintenance;

namespace TightFlight.Controllers
{
    public class MaintenancesController : Controller
    {
        private readonly FlightContext _context;
        private IEmployee _employees;

        public MaintenancesController(FlightContext context, IEmployee Employees)
        {
            _context = context;
            _employees = Employees;
        }

        public string returnEmployeeFullName(int x)
        {
            return _employees.GetFirstName(_context.MaintenanceEmployees.FirstOrDefault(me => me.MaintenanceId == x).EmployeeId)
                + _employees.GetLastName(_context.MaintenanceEmployees.FirstOrDefault(me => me.MaintenanceId == x).EmployeeId)
                + ", "
                + _employees.GetFirstName(_context.MaintenanceEmployees.Where(me => me.MaintenanceId == x).Skip(1).Single().EmployeeId)
                + _employees.GetLastName(_context.MaintenanceEmployees.Where(me => me.MaintenanceId == x).Skip(1).Single().EmployeeId)
                + ", "
                + _employees.GetFirstName(_context.MaintenanceEmployees.Where(me => me.MaintenanceId == x).Skip(2).Single().EmployeeId)
                + _employees.GetLastName(_context.MaintenanceEmployees.Where(me => me.MaintenanceId == x).Skip(2).Single().EmployeeId)
                + ", "
                + _employees.GetFirstName(_context.MaintenanceEmployees.Where(me => me.MaintenanceId == x).Skip(3).Single().EmployeeId)
                + _employees.GetLastName(_context.MaintenanceEmployees.Where(me => me.MaintenanceId == x).Skip(3).Single().EmployeeId)
                + ", "
                + _employees.GetFirstName(_context.MaintenanceEmployees.Where(me => me.MaintenanceId == x).Skip(4).Single().EmployeeId)
                + _employees.GetLastName(_context.MaintenanceEmployees.Where(me => me.MaintenanceId == x).Skip(4).Single().EmployeeId)
                ;
        }

        // GET: Maintenances
        public IActionResult Index()
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }

            var maintenanceModels = _context.Maintenances
                .Include(maintenance => maintenance.MaintenanceType)
                .Include(maintenance => maintenance.Plane);

            List<Employee> EmployeesList = _context.MaintenanceEmployees.Select(me => me.Employee).ToList();


            var listingResult = maintenanceModels
                .Select(result => new TightFlight.Models.Maintenance.Maintenance
                {
                    //EmployeeName = result.Employee.LastName + " " + result.Employee.LastName,
                    EmployeeName = "",
                    Date = result.Date,
                    MaintenanceType = result.MaintenanceType.Description,
                    PlaneModel = result.Plane.Model,
                    PlaneType = (result.Plane.Type == 1) ? "Passengder" : "Cargo"
                });

            List<TightFlight.Models.Maintenance.Maintenance> newlist = listingResult.ToList();

            for (int i = 0; i < newlist.Count(); i++)
            {
                newlist[i].EmployeeName = EmployeesList[i].FirstName + " " + EmployeesList[i].LastName + ", "
                    + EmployeesList[i + 1].FirstName + " " + EmployeesList[i + 1].LastName + ", "
                    + EmployeesList[i + 2].FirstName + " " + EmployeesList[i + 2].LastName + ", "
                    + EmployeesList[i + 3].FirstName + " " + EmployeesList[i + 3].LastName + ", "
                    + EmployeesList[i + 4].FirstName + " " + EmployeesList[i + 4].LastName;
            }

            var model = new MaintenanceIndex
            {
                Maintenances = newlist
            };

            return View(model);
        }


    }
}