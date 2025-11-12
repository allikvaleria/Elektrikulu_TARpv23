namespace Elektrikulu_TARpv23.Models
{
    public class Consumer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
