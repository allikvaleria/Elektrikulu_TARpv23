using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<List<Device>> PostDevice([FromBody] Device device)
        {
            if (device.DateOfNextMaintenance < DateTime.Now)
                return BadRequest("Hooldusaeg ei tohi olla minevikus");

            if (device.ResidualValue > device.PurchasePrice)
                return BadRequest("Jääkmaksumus ei tohi olla suurem kui soetusmaksumus");

            _context.Devices.Add(device);
            _context.SaveChanges();

            return Ok(_context.Devices.ToList());
        }

        [HttpPut("{id}")]
        public ActionResult<List<Device>> PutDevice(int id, [FromBody] Device updatedDevice)
        {
            var device = _context.Devices.Find(id);
            if (device == null)
                return NotFound();

            if (updatedDevice.DateOfNextMaintenance < DateTime.Now)
                return BadRequest("Hooldusaeg ei tohi olla minevikus");

            if (updatedDevice.ResidualValue > updatedDevice.PurchasePrice)
                return BadRequest("Jääkmaksumus ei tohi olla suurem kui soetusmaksumus");

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
        public ActionResult<List<Device>> DeleteDevice(int id)
        {
            var device = _context.Devices.Find(id);
            if (device == null)
                return NotFound();

            _context.Devices.Remove(device);
            _context.SaveChanges();

            return Ok(_context.Devices.ToList());
        }

        [HttpGet("maintenance-due")]
        public List<Device> GetDevicesForMaintenance()
        {
            return _context.Devices
                .Where(d => d.DateOfNextMaintenance < DateTime.Now)
                .ToList();
        }

        [HttpGet("total-purchase-price")]
        public double GetTotalPurchasePrice()
        {
            return _context.Devices.Sum(d => d.PurchasePrice);
        }

        [HttpGet("total-residual-value")]
        public double GetTotalResidualValue()
        {
            return _context.Devices.Sum(d => d.ResidualValue);
        }

        [HttpGet("active")]
        public List<Device> GetActiveDevices()
        {
            return _context.Devices.Where(d => d.ActiveBinaryValue == true).ToList();
        }

        [HttpGet("inactive")]
        public List<Device> GetInactiveDevices()
        {
            return _context.Devices.Where(d => d.ActiveBinaryValue == false).ToList();
        }
    }
}
