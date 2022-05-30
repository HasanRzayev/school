using System;
using System.Collections.Generic;

namespace school.Models
{
    public partial class Student
    {
        public Student()
        {
            Attendances = new HashSet<Attendance>();
            RideStudents = new HashSet<RideStudent>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Home { get; set; } = null!;
        public string HomeDescription { get; set; } = null!;
        public string OtherAddress { get; set; } = null!;
        public string OtherAddressDescription { get; set; } = null!;
        public int ParentId { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual Parent Parent { get; set; } = null!;
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<RideStudent> RideStudents { get; set; }
    }
}
