using System.ComponentModel.DataAnnotations;

namespace FlightData.Models
{
    public class MaintenanceEmployee
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        
        public int MaintenanceId { get; set; }
        public Maintenance Maintenance { get; set; }
    }
}
