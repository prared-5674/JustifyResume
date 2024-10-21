using FindDevice.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FindDevice.Controllers
{
    [ApiController]
    [Route("api/device")]
    public class DeviceController : ControllerBase
    {
        private static Dictionary<string, List<string>> tenantData = new Dictionary<string, List<string>>();

        // POST: api/device/register
        [HttpPost("register")]
        public IActionResult RegisterDevice([FromBody] DeviceModel device, [FromHeader(Name = "tenantId")] string tenantId)
        {
            if (!tenantData.ContainsKey(tenantId))
                tenantData[tenantId] = new List<string>();

            tenantData[tenantId].Add(device.DeviceName);
            return Ok("Device Registered Successfully!");
        }

        // GET: api/device/data/{tenantId}
        [HttpGet("data/{tenantId}")]
        public IActionResult GetTenantData(string tenantId)
        {
            if (tenantData.ContainsKey(tenantId))
                return Ok(tenantData[tenantId]);
            return NotFound("Tenant Not Found");
        }
    }
}
