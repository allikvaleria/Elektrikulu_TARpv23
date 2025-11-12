using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Elektrikulu_TARpv23.Data;
using Elektrikulu_TARpv23.Models;

namespace Elektrikulu_TARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentStatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<PaymentStatus> GetStatuses()
        {
            return _context.PaymentStatuss.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<PaymentStatus> GetStatus(int id)
        {
            var status = _context.PaymentStatuss.Find(id);
            if (status == null)
                return NotFound();
            return status;
        }

        [HttpPost]
        public List<PaymentStatus> PostStatus([FromBody] PaymentStatus status)
        {
            _context.PaymentStatuss.Add(status);
            _context.SaveChanges();
            return _context.PaymentStatuss.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<PaymentStatus>> PutStatus(int id, [FromBody] PaymentStatus updatedStatus)
        {
            var status = _context.PaymentStatuss.Find(id);
            if (status == null)
                return NotFound();

            status.IsPaid = updatedStatus.IsPaid;
            status.DueDate = updatedStatus.DueDate;
            status.PaidAmount = updatedStatus.PaidAmount;
            status.PaymentDate = updatedStatus.PaymentDate;

            _context.PaymentStatuss.Update(status);
            _context.SaveChanges();

            return Ok(_context.PaymentStatuss.ToList());
        }

        [HttpDelete("{id}")]
        public List<PaymentStatus> DeleteStatus(int id)
        {
            var status = _context.PaymentStatuss.Find(id);
            if (status == null)
                return _context.PaymentStatuss.ToList();

            _context.PaymentStatuss.Remove(status);
            _context.SaveChanges();

            return _context.PaymentStatuss.ToList();
        }
    }
}

