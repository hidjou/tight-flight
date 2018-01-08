using FlightData;
using FlightData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightServices
{
    public class EmployeeService : IEmployee
    {
        private FlightContext _context;

        public EmployeeService(FlightContext context)
        {
            _context = context;
        }
        // POST
        public void Add(Employee NewEmployee)
        {
            // define function for generating a random number
            const string chars = "0123456789";
            Random random = new Random();
            string RandomString(int length)
            {
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }

            bool noMatch = true;
            // Fetch all emplyee numbers
            var employeeNumbers = _context.Employees.Select(employee => employee.EmployeeNumber).ToList();
            while (noMatch)
            {
                // If random number doesnt exist in database, bind it and persist newEmployee
                string randomNumber = RandomString(5);
                if (!employeeNumbers.Contains(randomNumber))
                {
                    NewEmployee.EmployeeNumber = randomNumber;
                    noMatch = false;
                }
            }

            _context.Add(NewEmployee);
            _context.SaveChanges();
        }
        // UPDATE
        public void Update(int Id, Employee Employee)
        {
            Employee oldEmployee = GetById(Id);
            oldEmployee.FirstName = Employee.FirstName;
            oldEmployee.LastName = Employee.LastName;
            oldEmployee.Job = Employee.Job;
            oldEmployee.TotalHours = Employee.TotalHours;
            _context.SaveChanges();
        }
        // DELETE
        public void Delete(int Id)
        {
            _context.Remove(GetById(Id));
            _context.SaveChanges();
        }
        public void AddHours(int Id, int Hours)
        {
            GetById(Id).TotalHours += Hours;
            _context.SaveChanges();
        }
        public void ChangeStatus(int Id)
        {
            if (GetById(Id).Status == 0) GetById(Id).Status = 1;
            else GetById(Id).Status = 0;
            _context.SaveChanges();
        }
        // GET
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees;
        }
        // GET ONE
        public Employee GetById(int Id)
        {
            return GetAll()
                .FirstOrDefault(employee => employee.Id == Id);
        }

        public string GetFirstName(int Id)
        {
            return _context.Employees
                    .FirstOrDefault(employee => employee.Id == Id).FirstName;
        }
        public string GetLastName(int Id)
        {
            return _context.Employees
                    .FirstOrDefault(employee => employee.Id == Id).LastName;
        }
        public string GetFullName(int Id)
         {
             Employee Employee = _context.Employees
                     .FirstOrDefault(employee => employee.Id == Id);
             return Employee.FirstName + " " + Employee.LastName;
         }
    public string GetJob(int Id)
        {
            int job = _context.Employees
                    .FirstOrDefault(employee => employee.Id == Id).Job;

            if(job == 1) { return "Flight-deck crew"; }
            else if(job == 2) { return "Cabin crew"; }
            else { return "Ground crew"; }
        }
        public string GetEmployeeNumber(int Id)
        {
            return _context.Employees
                .FirstOrDefault(employee => employee.Id == Id).EmployeeNumber;
        }
        public int GetTotalHours(int Id)
        {
            return _context.Employees
                .FirstOrDefault(employee => employee.Id == Id).TotalHours;
        }
        public int GetStatus(int Id)
        {
            return GetById(Id).Status;
        }
    }
}
