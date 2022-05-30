using System;
using System.Collections.Generic;

namespace school.Models
{
    public partial class Driver
    {
        public Driver()
        {
            Rides = new HashSet<Ride>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string License { get; set; } = null!;

        public virtual Car? Car { get; set; }
        public virtual ICollection<Ride> Rides { get; set; }
        public override string ToString()
        {
            return this.UserName;
        }
    }
}
