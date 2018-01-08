using FlightData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightData
{
    public interface IFlight
    {
        void Add(string Origin, string Destination, DateTime Departure, int Type);
        void Add(Flight NewFlight);
        IEnumerable<Flight> GetAll();
        Flight GetById(int Id);

        List<Employee> RandomEmployees(int NumberOfEmployees, IEnumerable<Employee> Enumerable);
        void AssignEmployees(List<Employee> Employees, int FlightDuration);
        void AddEntriesToFlightEmployees(List<Employee> Employees, int FlightId);
        int FindFlightDuration(string Origin, string Destination);

        string GetOrigin(int Id);
        string GetDestination(int Id);
        int GetDuration(int Id);
        string GetStatus(int Id);
        DateTime GetDeparture(int Id);

        int RandomIndexFromRange(int Range);

        // Continuious checks
        void CheckFlightsForTakeOff();
        void CheckFlightsForLanding();
        void CheckPlanesForOffMaintenance();

        // Actions on flights and planes
        void TakeOff(int Id);
        void Land(int Id);
        void EndMaintenance(int Id);
        void MaintainPlane(int Id);

        IEnumerable<string> GetCities();
        void GenerateRandomFlight();
    }
}
