namespace Sb.Models.Entities
{
    public class Car : Entity
    {
        public string Title { get; set; }
        public string Number { get; set; }
        public int SeatCount { get; set; }
        public Driver Driver { get; set; }
        public int? DriverId { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
