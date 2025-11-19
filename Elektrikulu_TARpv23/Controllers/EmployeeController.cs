using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elektrikulu_TARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetEmployees([FromQuery] int page = 1)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"https://reqres.in/api/users?page={page}").Result;

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Error receiving data from external API");
                }

                var content = response.Content.ReadAsStringAsync().Result;
                return Content(content, "application/json");
            }
        }
    }
}
