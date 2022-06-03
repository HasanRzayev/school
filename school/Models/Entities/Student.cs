using System.Collections.Generic;

namespace Sb.Models.Entities
{
    public class Student : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Home { get; set; }
        public string HomeDescription { get; set; }
        public string OtherAddress { get; set; }
        public string OtherAddressDescription { get; set; }
        public Parent Parent { get; set; }
        public int ParentId { get; set; }
        public Group Group { get; set; }
        public int? GroupId { get; set; }
        public int? RideId { get; set; }
        public Ride? Ride { get; set; }
        public virtual List<Attendance> Attendances { get; set; }

    }
}
