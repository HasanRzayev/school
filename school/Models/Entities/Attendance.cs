using System;

namespace Sb.Models.Entities
{
    public class Attendance : Entity
    {
        public bool WillAttend { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Destination { get; set; } = "home";
        public Student Student { get; set; }
        public int StudentId { get; set; }
    }
}
