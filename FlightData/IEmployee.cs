using FlightData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightData
{
    public interface IEmployee
    {
        void Add(Employee NewEmployee);
        void Update(int Id, Employee Employee);
        void Delete(int Id);
        IEnumerable<Employee> GetAll();
        Employee GetById(int Id);


        void AddHours(int Id, int Hours);
        void ChangeStatus(int Id);

        string GetFirstName(int Id);
        string GetLastName(int Id);
        string GetFullName(int Id);
        string GetJob(int Id);
        string GetEmployeeNumber(int Id);
        int GetTotalHours(int Id);
        int GetStatus(int Id);
    }
}
