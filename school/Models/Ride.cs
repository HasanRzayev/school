using System;
using System.Collections.Generic;

namespace school.Models
{
    public partial class Ride
    {
        public Ride()
        {
            RideStudents = new HashSet<RideStudent>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public int DriverId { get; set; }

        public virtual Driver Driver { get; set; } = null!;
        public virtual ICollection<RideStudent> RideStudents { get; set; }
    }
}
