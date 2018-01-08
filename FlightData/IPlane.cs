using FlightData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightData
{
    public interface IPlane
    {
        void Add(Plane newPlane);
        void Update(int id, Plane plane);
        void Delete(int id);
        IEnumerable<Plane> GetAll();
        Plane GetById(int id);

        string GetIdentifier(int id);
        int GetMaxCapacity(int id);
        int GetType(int id);
        string GetManufacturer(int id);
        string GetModel(int id);
        string GetStatus(int id);
        DateTime GetYear(int id);
        int GetFlyingHours(int id);

        void AddHours(int Id, int Hours);
    }
}
