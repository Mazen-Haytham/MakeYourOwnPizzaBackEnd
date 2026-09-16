using System.Collections.Generic;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Drivers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourOwnPizza.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [Authorize(Roles="Manager,Delivery")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverResponse>>> GetDrivers()
        {
            var drivers = await _driverService.GetAllDriversAsync();
            return Ok(drivers);
        }
    }
}
