namespace Elektrikulu_TARpv23.Models
{
    public class Contact
    {
        internal object Consumer;
        public int Id { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int ConsumerId { get; set; }
    }
}
