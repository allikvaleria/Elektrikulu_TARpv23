namespace Elektrikulu_TARpv23.Models
{
    public class PaymentStatus
    {
        public int Id { get; set; }
        public bool IsPaid { get; set; }
        public DateTime DueDate { get; set; }
        public double PaidAmount { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
