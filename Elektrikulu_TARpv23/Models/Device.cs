namespace Elektrikulu_TARpv23.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public DateTime DateOfNextMaintenance { get; set; }
        public double ResidualValue { get; set; }
        public double PurchasePrice { get; set; }
        public bool ActiveBinaryValue { get; set; }
    }
}
