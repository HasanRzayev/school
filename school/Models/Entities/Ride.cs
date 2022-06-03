using System.Collections.Generic;

namespace Sb.Models.Entities
{
    public class Ride : Entity
    {
        public string Type { get; set; }
        public Driver Driver { get; set; }
        public int DriverId { get; set; }
        public virtual List<Student> Students { get; set; }

    }
}
