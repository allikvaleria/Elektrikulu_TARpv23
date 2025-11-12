using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Elektrikulu_TARpv23.Data;
using Elektrikulu_TARpv23.Models;

namespace Elektrikulu_TARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Contact> GetContacts()
        {
            return _context.Contacts.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Contact> GetContact(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
                return NotFound();
            return contact;
        }

        [HttpPost]
        public List<Contact> PostContact([FromBody] Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return _context.Contacts.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Contact>> PutContact(int id, [FromBody] Contact updatedContact)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
                return NotFound();

            contact.Address = updatedContact.Address;
            contact.Phone = updatedContact.Phone;
            contact.ConsumerId = updatedContact.ConsumerId;

            _context.Contacts.Update(contact);
            _context.SaveChanges();

            return Ok(_context.Contacts.ToList());
        }

        [HttpDelete("{id}")]
        public List<Contact> DeleteContact(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
                return _context.Contacts.ToList();

            _context.Contacts.Remove(contact);
            _context.SaveChanges();

            return _context.Contacts.ToList();
        }
    }
}
