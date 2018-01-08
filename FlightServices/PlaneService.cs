using FlightData;
using System;
using FlightData.Models;
using System.Collections.Generic;
using System.Linq;

namespace FlightServices
{
    public class PlaneService : IPlane
    {
        private FlightContext _context;

        public PlaneService(FlightContext context)
        {
            _context = context;
        }
        // POST
        public void Add(Plane newPlane)
        {
            // define function for generating a random identifier
            const string LetterPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string NumberPool = "1234567890";
            Random random = new Random();
            string RandomString(int Length)
            {
                string part1 = new string(Enumerable.Repeat(NumberPool, Length / 2)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
                string part2 = new string(Enumerable.Repeat(LetterPool, Length / 2)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                return part1 + part2;
            }

            bool NoMatch = true;
            // Fetch all identifiers
            var Identifiers = _context.Planes.Select(plane => plane.Identifier).ToList();
            while (NoMatch)
            {
                // If random identifier doesnt exist in database, bind it and persist newPlane
                string RandomIndentifier = RandomString(6);
                if (!Identifiers.Contains(RandomIndentifier))
                {
                    newPlane.Identifier = RandomIndentifier;
                    NoMatch = false;
                }
            }

            _context.Add(newPlane);
            _context.SaveChanges();
        }
        // UPDATE
        public void Update(int id, Plane plane)
        {
            Plane oldPlane = GetById(id);
            oldPlane.MaxCapacity = plane.MaxCapacity;
            oldPlane.Type = plane.Type;
            oldPlane.Manufacturer = plane.Manufacturer;
            oldPlane.Model = plane.Model;
            oldPlane.Year = plane.Year;
            oldPlane.FlyingHours = plane.FlyingHours;
            _context.SaveChanges();
        }
        // DELETE
        public void Delete(int id)
        {
            _context.Remove(GetById(id));
            _context.SaveChanges();
        }
        // GET
        public IEnumerable<Plane> GetAll()
        {
            return _context.Planes;
        }
        // GET ONE
        public Plane GetById(int id)
        {
            return GetAll()
                .FirstOrDefault(plane => plane.Id == id);
        }

        public string GetIdentifier(int id)
        {
            return _context.Planes
                    .FirstOrDefault(plane => plane.Id == id).Identifier;
        }
        public int GetMaxCapacity(int id)
        {
            return _context.Planes
                    .FirstOrDefault(plane => plane.Id == id).MaxCapacity;
        }
        public int GetType(int id)
        {
            return _context.Planes
                    .FirstOrDefault(plane => plane.Id == id).Type;
        }
        
        public string GetManufacturer(int id)
        {
            return _context.Planes
                    .FirstOrDefault(plane => plane.Id == id).Manufacturer;
        }
        public string GetModel(int id)
        {
            return _context.Planes
                    .FirstOrDefault(plane => plane.Id == id).Model;
        }
        public string GetStatus(int id)
        {
            int Status = _context.Planes.FirstOrDefault
                (plane => plane.Id == id).Status;
            if (Status == 0) return "Free";
            else if (Status == 1) return "Flying";
            else return "Under maintenance";
        }
        public DateTime GetYear(int id)
        {
            return _context.Planes
                    .FirstOrDefault(plane => plane.Id == id).Year;
        }
        public int GetFlyingHours(int id) {
            return _context.Planes
                    .FirstOrDefault(plane => plane.Id == id).FlyingHours;
        }

        public void AddHours(int Id, int Hours)
        {
            GetById(Id).FlyingHours += Hours;
            _context.SaveChanges();
        }
    }
}
