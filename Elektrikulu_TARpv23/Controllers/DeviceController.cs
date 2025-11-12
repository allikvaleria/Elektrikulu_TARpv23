using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Elektrikulu_TARpv23.Data;
using Elektrikulu_TARpv23.Models;

namespace Elektrikulu_TARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DeviceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Device> GetDevices()
        {
            return _context.Devices.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Device> GetDevice(int id)
        {
            var device = _context.Devices.Find(id);
            if (device == null)
                return NotFound();
            return device;
        }

        [HttpPost]
        public List<Device> PostDevice([FromBody] Device device)
        {
            _context.Devices.Add(device);
            _context.SaveChanges();
            return _context.Devices.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Device>> PutDevice(int id, [FromBody] Device updatedDevice)
        {
            var device = _context.Devices.Find(id);
            if (device == null)
                return NotFound();

            device.Name = updatedDevice.Name;
            device.Manufacturer = updatedDevice.Manufacturer;
            device.DateOfNextMaintenance = updatedDevice.DateOfNextMaintenance;
            device.ResidualValue = updatedDevice.ResidualValue;
            device.PurchasePrice = updatedDevice.PurchasePrice;
            device.ActiveBinaryValue = updatedDevice.ActiveBinaryValue;

            _context.Devices.Update(device);
            _context.SaveChanges();

            return Ok(_context.Devices.ToList());
        }

        [HttpDelete("{id}")]
        public List<Device> DeleteDevice(int id)
        {
            var device = _context.Devices.Find(id);
            if (device == null)
                return _context.Devices.ToList();

            _context.Devices.Remove(device);
            _context.SaveChanges();

            return _context.Devices.ToList();
        }
    }
}
