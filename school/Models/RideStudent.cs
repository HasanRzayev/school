using System;
using System.Collections.Generic;

namespace school.Models
{
    public partial class RideStudent
    {
        public int Id { get; set; }
        public int RideId { get; set; }
        public int StudentId { get; set; }
        public string Status { get; set; } = null!;

        public virtual Ride Ride { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
