using System;
using System.Collections.Generic;

namespace school.Models
{
    public partial class Group
    {
        public Group()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public override string ToString()
        {
            return Title;
        }
        public virtual ICollection<Student> Students { get; set; }
    }
}
