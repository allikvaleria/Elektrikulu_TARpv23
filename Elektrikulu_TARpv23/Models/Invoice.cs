namespace Elektrikulu_TARpv23.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public double AmountPaid { get; set; }
        public double TotalAmount { get; set; }
        public int PaymentStatusId { get; set; }
        public PaymentStatus Status { get; set; }
        public int ConsumerId { get; set; }
        public Consumer Consumer { get; set; }
    }
}
