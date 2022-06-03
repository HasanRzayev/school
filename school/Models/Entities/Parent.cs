using System.Collections.Generic;

namespace Sb.Models.Entities
{
    public class Parent : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Student> Children { get; set; }
        public override string ToString()
        {
            return $"{this.LastName} {this.FirstName}";
        }
    }
}
