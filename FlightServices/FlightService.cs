using FlightData;
using System;
using System.Collections.Generic;
using System.Text;
using FlightData.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FlightServices
{
    public class FlightService : IFlight
    {
        private FlightContext _context;
        private IPlane _planes;
        private IEmployee _employees;

        public FlightService(FlightContext Context, IPlane Planes, IEmployee Employees)
        {
            _context = Context;
            _planes = Planes;
            _employees = Employees;
        }

        public List<Employee> RandomEmployees(int NumberOfEmployees, IEnumerable<Employee> Enumerable)
        {
            var employeeList = Enumerable as IList<Employee> ?? Enumerable.ToList();
            int Range = employeeList.Count();

            List<int> RandomIndicies = new List<int>();
            Random Random = new Random();
            int MyNumber = 0;

            for (int i = 0; i < NumberOfEmployees; i++)
            {
                MyNumber = Random.Next(0, Range);
                // As long as our list contains this number, we generate a new one to avoid duplicates
                while (RandomIndicies.Contains(MyNumber))
                {
                    MyNumber = Random.Next(0, Range);
                }
                RandomIndicies.Add(MyNumber);
            }

            List<Employee> EmployeesToBeAssigned = new List<Employee>();
            for (int i = 0; i < RandomIndicies.Count(); i++)
            {
                Employee HiredEmployee = employeeList[RandomIndicies[i]];
                EmployeesToBeAssigned.Add(HiredEmployee);
            }
            return EmployeesToBeAssigned;
        }

        public void AssignEmployees(List<Employee> Employees, int FlightDuration)
        {
            for (int i = 0; i < Employees.Count(); i++)
            {
                _employees.ChangeStatus(Employees[i].Id);
                _employees.AddHours(Employees[i].Id, FlightDuration / 3600);
            }
        }

        public void AddEntriesToFlightEmployees(List<Employee> Employees, int FlightId)
        {
            for (int i = 0; i < Employees.Count(); i++)
            {
                FlightEmployee NewFlightEmployee = new FlightEmployee
                {
                    FlightId = FlightId,
                    EmployeeId = Employees[i].Id
                };
                _context.Add(NewFlightEmployee);
                _context.SaveChanges();
            }
        }
        public int RandomIndexFromRange(int Range)
        {
            Random Random = new Random();
            int RandomIndex = Random.Next(0, Range);
            return RandomIndex;
        }
        // POST
        public void Add(string Origin, string Destination, DateTime Departure, int Type)
        {
            IEnumerable<Plane> FreePlanes = _context.Planes.Where(plane => plane.Status == 0).Where(plane => plane.Type == Type);
            List<Plane> FreePlanesList = FreePlanes.ToList();
            Plane RandomPlane = FreePlanesList[RandomIndexFromRange(FreePlanesList.Count())];

            Flight NewFlight = new Flight
            {
                Origin = Origin,
                Destination = Destination,
                Departure = Departure,
                Duration = FindFlightDuration(Origin, Destination),
                Status = 0,
                PlaneId = RandomPlane.Id
            };

            Add(NewFlight);
        }
        public void Add(Flight NewFlight)
        {
            NewFlight.Plane = _planes.GetById(NewFlight.PlaneId);
            NewFlight.Duration = FindFlightDuration(NewFlight.Origin, NewFlight.Destination) / Config.Scale;

            _context.Add(NewFlight);
            _context.SaveChanges();
            
        }
        public int FindFlightDuration(string Origin, string Destination)
        {
            // Find a flight that is between these two locations (doesnt matter which is the origin and which is the destination, the duration is the same)
            string LowerCaseOrigin = Origin.ToLower();
            string LowerCaseDestination = Destination.ToLower();

            IEnumerable<FlightDuration> FlightDurations = _context.FlightDurations
                .Where(flightduration => flightduration.Origin == LowerCaseOrigin)
                .Where(flightduration => flightduration.Destination == LowerCaseDestination);

            if (FlightDurations.Count() > 0)
            {
                return FlightDurations.First().Duration;
            }
            else
            {
                return _context.FlightDurations.Where(flightduration => flightduration.Origin == LowerCaseDestination).Where(flightduration => flightduration.Destination == LowerCaseOrigin).First().Duration;
            }
        }
        public bool CheckFlightPathValidity(string Origin, string Destination)
        {
            IEnumerable<FlightDuration> OriginToDestinationFlight = _context.FlightDurations
                .Where(flightduration => flightduration.Origin == Origin)
                .Where(flightduration => flightduration.Destination == Destination);

            IEnumerable<FlightDuration> DestinationToOriginFlight = _context.FlightDurations
                .Where(flightduration => flightduration.Origin == Destination)
                .Where(flightduration => flightduration.Destination == Origin);

            if (OriginToDestinationFlight.Count() > 0)
            {
                return true;
            }
            else if (DestinationToOriginFlight.Count() > 0)
            {
                return true;
            }
            else return false;
        }
        public void GenerateRandomFlight()
         {
             IEnumerable<string> FlightOrigins = _context.FlightDurations.Select(flightduration => flightduration.Origin);
             IEnumerable<string> FlightDestinations = _context.FlightDurations.Select(flightduration => flightduration.Destination);
 
             List<string> FlightOriginsList = FlightOrigins.Distinct().ToList();
            List<string> FlightDestinationsList = FlightDestinations.Distinct().ToList();

            string ValidOrigin = FlightOriginsList[RandomIndexFromRange(FlightOriginsList.Count())];
            string ValidDestination = FlightDestinationsList[RandomIndexFromRange(FlightDestinationsList.Count())];

            while(CheckFlightPathValidity(ValidOrigin, ValidDestination) == false)
            {
                ValidOrigin = FlightOriginsList[RandomIndexFromRange(FlightOriginsList.Count())];
                ValidDestination = FlightDestinationsList[RandomIndexFromRange(FlightDestinationsList.Count())];
            }

             Add(ValidOrigin,
                 ValidDestination,
                 DateTime.Now.AddSeconds(RandomIndexFromRange(300) + 60),
                 RandomIndexFromRange(1) + 1);
         }
        // GET
        public IEnumerable<Flight> GetAll()
        {
            return _context.Flights
                .Include(flight => flight.Plane);
        }
        // GET ONE
        public Flight GetById(int Id)
        {
            return GetAll()
                .FirstOrDefault(Flight => Flight.Id == Id);
        }

        public string GetOrigin(int Id)
        {
            return _context.Flights
                    .FirstOrDefault(flight => flight.Id == Id).Origin;
        }
        public string GetDestination(int Id)
        {
            return _context.Flights
                    .FirstOrDefault(flight => flight.Id == Id).Destination;
        }
        public int GetDuration(int Id)
        {
            return _context.Flights
                    .FirstOrDefault(flight => flight.Id == Id).Duration / Config.Scale;
        }
        public string GetStatus(int Id)
        {
            int Status = _context.Flights
                .FirstOrDefault(flight => flight.Id == Id).Status;

            if (Status == 0) { return "Yet to depart"; }
            else if (Status == 1) { return "Under way"; }
            else { return "Landed"; }
        }
        public DateTime GetDeparture(int Id)
        {
            return _context.Flights
                .FirstOrDefault(flight => flight.Id == Id).Departure;
        }

        public void CheckFlightsForTakeOff()
        {
            IEnumerable<Flight> PlannedFlights = _context.Flights.Where(flight => flight.Status == 0).Where(flight => flight.Departure <= DateTime.Now);

            if(PlannedFlights.Count() > 0)
            {
                List<Flight> PlannedFlightList = PlannedFlights.ToList();

                for(int i = 0; i < PlannedFlights.Count(); i++)
                {
                    TakeOff(PlannedFlightList[i].Id);
                }
            }
        }

        public void TakeOff(int Id)
        {
            Flight FlightToTakeOff = GetById(Id);

            List<Employee> FlightCrew = new List<Employee>();

            // Check PLANE object
            if (FlightToTakeOff.Plane.Type == 1)
            {
                // Passenger flight
                IEnumerable<Employee> FreeFlightDeckEmployees = _context
                .Employees.Where(employee => employee.Status == 0).Where(employee => employee.Job == 1);
                IEnumerable<Employee> FreeCabinEmployees = _context
                    .Employees.Where(employee => employee.Status == 0).Where(employee => employee.Job == 2);

                FlightCrew = RandomEmployees(2, FreeFlightDeckEmployees);
                List<Employee> CabinCrew = RandomEmployees(3, FreeCabinEmployees);
                FlightCrew.AddRange(CabinCrew);
            }
            else
            {
                // Cargo flight
                IEnumerable<Employee> FreeFlightDeckEmployees = _context
                .Employees.Where(employee => employee.Status == 0).Where(employee => employee.Job == 1);

                FlightCrew = RandomEmployees(3, FreeFlightDeckEmployees);
            }

            FlightToTakeOff.Plane.Status = 1;
            FlightToTakeOff.Status = 1;

            _context.SaveChanges();

            AssignEmployees(FlightCrew, FlightToTakeOff.Duration);
            AddEntriesToFlightEmployees(FlightCrew, FlightToTakeOff.Id);
        }
        
        public void CheckFlightsForLanding()
        {
            IEnumerable<Flight> LandingFlights = _context.Flights.Where(flight => flight.Status == 1).Where(flight => (flight.Departure.AddSeconds(flight.Duration / Config.Scale)) <= DateTime.Now);

            if (LandingFlights.Count() > 0)
            {
                List<Flight> LandingFlightsList = LandingFlights.ToList();

                for (int i = 0; i < LandingFlightsList.Count(); i++)
                {
                    Land(LandingFlightsList[i].Id);
                }
            }
        }

        public void Land(int Id)
        {
            Flight LandingFlight = GetById(Id);

            LandingFlight.Status = 2;
            LandingFlight.Plane.Status = 0;

            // Fetch flight employees
            IEnumerable<FlightEmployee> FlightEmployeeObjects = _context.FlightEmployees.Where(flightemployee => flightemployee.FlightId == LandingFlight.Id);
            List<FlightEmployee> FlightEmployeeObjectsList = FlightEmployeeObjects.ToList();

            // Edit employees' status and add hours
            for (int i = 0; i < FlightEmployeeObjectsList.Count(); i++)
            {
                _employees.ChangeStatus(FlightEmployeeObjectsList[i].EmployeeId);
                _employees.AddHours(FlightEmployeeObjectsList[i].EmployeeId, LandingFlight.Duration / Config.Scale);
            }

            // Add hours to plane
            Plane LandingPlane = LandingFlight.Plane;
            _planes.AddHours(LandingPlane.Id, LandingFlight.Duration / 3600);

            // Check if the plane needs assitance
            if (_planes.GetFlyingHours(LandingPlane.Id) >= 200)
            {
                IEnumerable<Maintenance> Maintenances = _context.Maintenances.Where(maintenance => maintenance.PlaneId == LandingPlane.Id);
                List<Maintenance> MaintenancesList = Maintenances.ToList();

                if ((Maintenances == null) || (_planes.GetFlyingHours(LandingPlane.Id) > (200 * MaintenancesList.Count())))
                {
                    MaintainPlane(LandingPlane.Id);
                }
            }
            _context.SaveChanges();
        }

        public void CheckPlanesForOffMaintenance()
        {
            IEnumerable<Maintenance> Maintenances = _context.Maintenances.Where(maintenance => maintenance.Date <= DateTime.Now.AddSeconds(-24 * 3600 / Config.Scale));
            List<Maintenance> MaintenancesList = Maintenances.ToList();

            List<Plane> PlanesToOffMaintenance = new List<Plane>();

            for (int i = 0; i < MaintenancesList.Count(); i++)
            {
                if (_planes.GetById(MaintenancesList[i].PlaneId).Status == 2)
                {
                    PlanesToOffMaintenance.Add(_planes.GetById(MaintenancesList[i].PlaneId));
                }
            }

            if (PlanesToOffMaintenance.Count() > 0)
            {
                for (int i = 0; i < PlanesToOffMaintenance.Count(); i++)
                {
                    EndMaintenance(PlanesToOffMaintenance[i].Id);
                }
            }
            _context.SaveChanges();
        }

        public void MaintainPlane(int Id)
        {
            Plane PlaneToBeMaintained = _planes.GetById(Id);

            IEnumerable<MaintenanceType> MaintenanceTypes = _context.MaintenanceTypes;
            List<MaintenanceType> MaintenanceTypesList = MaintenanceTypes.ToList();
            MaintenanceType MaintenanceType = MaintenanceTypesList[RandomIndexFromRange(MaintenanceTypesList.Count())];

            Maintenance maintenance = new Maintenance
            {
                PlaneId = PlaneToBeMaintained.Id,
                Plane = PlaneToBeMaintained,
                MaintenanceTypeId = MaintenanceType.Id,
                MaintenanceType = _context.MaintenanceTypes.FirstOrDefault(maintenancetype => maintenancetype.Id == MaintenanceType.Id),
                Date = DateTime.Now
            };

            _context.Add(maintenance);


            // Allocate ground crew
            IEnumerable<Employee> FreeGroundCrew = _context.Employees.Where(employee => employee.Status == 0).Where(employee => employee.Job == 3);
            IEnumerable<Employee> GroundCrew = RandomEmployees(5, FreeGroundCrew);
            List<Employee> GroundCrewList = GroundCrew.ToList();

            for (int i = 0; i < GroundCrewList.Count(); i++)
            {
                MaintenanceEmployee maintenanceEmployee = new MaintenanceEmployee
                {
                    EmployeeId = GroundCrewList[i].Id,
                    Employee = _employees.GetById(GroundCrewList[i].Id),
                    MaintenanceId = maintenance.Id,
                    Maintenance = maintenance
                };
                _context.Add(maintenanceEmployee);
                _employees.ChangeStatus(GroundCrewList[i].Id);
            }
            _context.SaveChanges();   
        }
        
        public void EndMaintenance(int Id)
        {
            Plane plane = _planes.GetById(Id);
            plane.Status = 0;

            Maintenance maintenance = _context.Maintenances.FirstOrDefault(Maintenance => Maintenance.PlaneId == plane.Id);
            IEnumerable<MaintenanceEmployee> MaintenanceEmployeeObjects = _context.MaintenanceEmployees.Where(maintenanceemployee => maintenanceemployee.MaintenanceId == maintenance.Id);
            List<MaintenanceEmployee> MaintenanceEmployeeObjectsList = MaintenanceEmployeeObjects.ToList();

            for(int i = 0; i < MaintenanceEmployeeObjectsList.Count(); i++)
            {
                _employees.ChangeStatus(MaintenanceEmployeeObjectsList[i].EmployeeId);
            }
        }
        public IEnumerable<string> GetCities()
         {
            IEnumerable<string> FlightOrigins = _context.FlightDurations.Select(flightduration => flightduration.Origin);
            IEnumerable<string> FlightDestinations = _context.FlightDurations.Select(flightduration => flightduration.Destination);
 
            IEnumerable<string> Cities = FlightOrigins.Union(FlightDestinations).ToList();
 
            return Cities;
         }
}
}
