using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerPlants.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PowerPlantsController : ControllerBase
    {
        private readonly ILogger<PowerPlantsController> _logger;

        public PowerPlantsController(ILogger<PowerPlantsController> logger)
        {
            _logger = logger;
        }
    }
}
