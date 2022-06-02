using System;
using System.Collections.Generic;

namespace school.Models
{
    public partial class Parent
    {
        public Parent()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public override string ToString()
        {
            return FirstName;
        }
        public virtual ICollection<Student> Students { get; set; }
    }
}
