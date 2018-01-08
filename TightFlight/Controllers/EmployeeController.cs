using FlightData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TightFlight.Models.Employee;

namespace TightFlight.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployee _employees;

        public EmployeeController(IEmployee employees)
        {
            _employees = employees;
        }

        // CREATE
        public IActionResult Create()
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(FlightData.Models.Employee newEmployee)
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var _newEmployee = new FlightData.Models.Employee
            {
                FirstName = newEmployee.FirstName,
                LastName = newEmployee.LastName,
                Job = newEmployee.Job,
                TotalHours = 0
            };
            _employees.Add(_newEmployee);
            return RedirectToAction("Index", "Employee");
        }

        // READ
        public IActionResult Index()
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var planeModels = _employees.GetAll();

            var listingResult = planeModels
                .Select(result => new EmployeeListingModel
                {
                    Id = result.Id,
                    FirstName = _employees.GetFirstName(result.Id),
                    LastName = _employees.GetLastName(result.Id),
                    Job = _employees.GetJob(result.Id),
                    EmployeeNumber = _employees.GetEmployeeNumber(result.Id),
                    Status = (_employees.GetStatus(result.Id) == 0) ? "Free" : "Working",
                    TotalHours = _employees.GetTotalHours(result.Id)  
                });

            var model = new EmployeeIndexModel()
            {
                Employees = listingResult
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
            var employeeModel = _employees.GetById(id);

            var formData = new EmployeeFormModel
            {
                Id = id,
                FirstName = employeeModel.FirstName,
                LastName = employeeModel.LastName,
                Job = employeeModel.Job,
                EmployeeNumber = employeeModel.EmployeeNumber,
                TotalHours = employeeModel.TotalHours
            };

            return View(formData);
        }

        public IActionResult Update(EmployeeFormModel EmployeeToBeEdited)
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            FlightData.Models.Employee _employee = _employees.GetById(EmployeeToBeEdited.Id);
            FlightData.Models.Employee newEmployee = new FlightData.Models.Employee
            {
                Id = EmployeeToBeEdited.Id,
                FirstName = EmployeeToBeEdited.FirstName,
                LastName = EmployeeToBeEdited.LastName,
                Job = EmployeeToBeEdited.Job,
                TotalHours = EmployeeToBeEdited.TotalHours
            };

            _employees.Update(_employee.Id, newEmployee);
            return RedirectToAction("Index", "Employee");
        }

        // DELETE
        public IActionResult Remove(int id)
        {
            if (User.IsInRole("Admin") == false)
            {
                return RedirectToAction("Index", "Home");
            }
            _employees.Delete(id);

            return RedirectToAction("Index", "Employee");
        }
    }
}
