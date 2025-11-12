using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Elektrikulu_TARpv23.Data;
using Elektrikulu_TARpv23.Models;

namespace Elektrikulu_TARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConsumerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Consumer> GetConsumers()
        {
            return _context.Customers.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Consumer> GetConsumer(int id)
        {
            var consumer = _context.Customers.Find(id);
            if (consumer == null)
                return NotFound();
            return consumer;
        }

        [HttpPost]
        public List<Consumer> PostConsumer([FromBody] Consumer consumer)
        {
            _context.Customers.Add(consumer);
            _context.SaveChanges();
            return _context.Customers.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Consumer>> PutConsumer(int id, [FromBody] Consumer updatedConsumer)
        {
            var consumer = _context.Customers.Find(id);
            if (consumer == null)
                return NotFound();

            consumer.Name = updatedConsumer.Name;
            consumer.ContactId = updatedConsumer.ContactId;
            consumer.LocationId = updatedConsumer.LocationId;

            _context.Customers.Update(consumer);
            _context.SaveChanges();

            return Ok(_context.Customers.ToList());
        }

        [HttpDelete("{id}")]
        public List<Consumer> DeleteConsumer(int id)
        {
            var consumer = _context.Customers.Find(id);
            if (consumer == null)
                return _context.Customers.ToList();

            _context.Customers.Remove(consumer);
            _context.SaveChanges();

            return _context.Customers.ToList();
        }
    }
}
