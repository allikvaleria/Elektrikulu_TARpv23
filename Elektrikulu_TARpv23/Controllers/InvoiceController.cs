using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Elektrikulu_TARpv23.Data;
using Elektrikulu_TARpv23.Models;

namespace Elektrikulu_TARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Invoice> GetInvoices()
        {
            return _context.Invoices.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Invoice> GetInvoice(int id)
        {
            var invoice = _context.Invoices.Find(id);
            if (invoice == null)
                return NotFound();
            return invoice;
        }

        [HttpPost]
        public List<Invoice> PostInvoice([FromBody] Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();
            return _context.Invoices.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Invoice>> PutInvoice(int id, [FromBody] Invoice updatedInvoice)
        {
            var invoice = _context.Invoices.Find(id);
            if (invoice == null)
                return NotFound();

            invoice.PaymentDate = updatedInvoice.PaymentDate;
            invoice.AmountPaid = updatedInvoice.AmountPaid;
            invoice.TotalAmount = updatedInvoice.TotalAmount;
            invoice.PaymentStatusId = updatedInvoice.PaymentStatusId;
            invoice.ConsumerId = updatedInvoice.ConsumerId;

            _context.Invoices.Update(invoice);
            _context.SaveChanges();

            return Ok(_context.Invoices.ToList());
        }

        [HttpDelete("{id}")]
        public List<Invoice> DeleteInvoice(int id)
        {
            var invoice = _context.Invoices.Find(id);
            if (invoice == null)
                return _context.Invoices.ToList();

            _context.Invoices.Remove(invoice);
            _context.SaveChanges();

            return _context.Invoices.ToList();
        }
        [HttpGet("unpaid")]
        public List<Invoice> GetAllUnpaidInvoices()
        {
            return _context.Invoices
                .Where(inv => inv.Status.IsPaid == false)
                .ToList();
        }

        [HttpGet("unpaid-overdue")]
        public List<Invoice> GetAllUnpaidOverdueInvoices()
        {
            return _context.Invoices
                .Where(inv => inv.Status.IsPaid == false && inv.Status.DueDate < DateTime.Now)
                .ToList();
        }

        [HttpGet("unpaid/customer/{customerId}")]
        public List<Invoice> GetCustomerUnpaidInvoices(int customerId)
        {
            return _context.Invoices
                .Where(inv => inv.ConsumerId == customerId && inv.Status.IsPaid == false)
                .ToList();
        }

        [HttpGet("unpaid-overdue/customer/{customerId}")]
        public List<Invoice> GetCustomerUnpaidOverdueInvoices(int customerId)
        {
            return _context.Invoices
                .Where(inv => inv.ConsumerId == customerId && inv.Status.IsPaid == false && inv.Status.DueDate < DateTime.Now)
                .ToList();
        }

        [HttpGet("customer/{customerId}")]
        public List<Invoice> GetCustomerInvoices(int customerId)
        {
            return _context.Invoices
                .Where(inv => inv.ConsumerId == customerId)
                .ToList();
        }

        [HttpGet("customer/{customerId}/total-sum")]
        public double GetCustomerInvoicesTotalSum(int customerId)
        {
            return _context.Invoices
                .Where(inv => inv.ConsumerId == customerId)
                .Sum(inv => inv.TotalAmount);
        }
    }
}
