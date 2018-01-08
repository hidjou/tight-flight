using FlightData.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FlightData
{
    public class FlightContext : IdentityDbContext<AuthUser, ApplicationRole, string>
    {
        public FlightContext(DbContextOptions options) : base(options) { }

        // Set Employees.TotalHours, Employees.Status, Flights.Status, Planes.Status and Planes.FlyingHours 
        // 'defaultValue' to 0 in migration before rolling

        public DbSet<Plane> Planes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightDuration> FlightDurations { get; set; }
        public DbSet<MaintenanceType> MaintenanceTypes { get; set; }

        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<MaintenanceEmployee> MaintenanceEmployees { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<FlightEmployee> FlightEmployees { get; set; }
    }

    public static class Config
    {
        public const int Scale = 60;
    }
}
