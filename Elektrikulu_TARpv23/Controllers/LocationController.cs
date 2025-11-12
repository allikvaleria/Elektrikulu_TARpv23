using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Elektrikulu_TARpv23.Data;
using Elektrikulu_TARpv23.Models;

namespace Elektrikulu_TARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LocationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Location> GetLocations()
        {
            return _context.Locations.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Location> GetLocation(int id)
        {
            var location = _context.Locations.Find(id);
            if (location == null)
                return NotFound();
            return location;
        }

        [HttpPost]
        public List<Location> PostLocation([FromBody] Location location)
        {
            _context.Locations.Add(location);
            _context.SaveChanges();
            return _context.Locations.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Location>> PutLocation(int id, [FromBody] Location updatedLocation)
        {
            var location = _context.Locations.Find(id);
            if (location == null)
                return NotFound();

            location.Street = updatedLocation.Street;
            location.House = updatedLocation.House;
            location.City = updatedLocation.City;
            location.PostalCode = updatedLocation.PostalCode;

            _context.Locations.Update(location);
            _context.SaveChanges();

            return Ok(_context.Locations.ToList());
        }

        [HttpDelete("{id}")]
        public List<Location> DeleteLocation(int id)
        {
            var location = _context.Locations.Find(id);
            if (location == null)
                return _context.Locations.ToList();

            _context.Locations.Remove(location);
            _context.SaveChanges();

            return _context.Locations.ToList();
        }
    }
}
