using System;
using System.Collections.Generic;

namespace school.Models
{
    public partial class Attendance
    {
        public int Id { get; set; }
        public bool WillAttend { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Destination { get; set; } = null!;
        public int StudentId { get; set; }

        public virtual Student Student { get; set; } = null!;
    }
}
