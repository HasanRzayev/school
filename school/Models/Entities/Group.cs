using System.Collections.Generic;

namespace Sb.Models.Entities
{
    public class Group : Entity
    {
        public string Title { get; set; }
        public virtual List<Student> Students { get; set; }
    }
}
