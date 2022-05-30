using System;
using System.Collections.Generic;

namespace school.Models
{
    public partial class Car
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Number { get; set; } = null!;
        public int SeatCount { get; set; }
        public int? DriverId { get; set; }

        public virtual Driver? Driver { get; set; }
        public override string ToString()
        {
            return this.Title;
        }
    }
}
