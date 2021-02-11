using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PowerPlants.Model;
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

        [HttpPost("productionplan")]
        public ActionResult<PowerPlantLoad[]> CalculateProductionPlan([FromBody] PowerPlantsRequest request)
        {
            // TODO: in real life validate input correctness. No correctness checks beyond this point.

            var powerPlantLoads = new List<PowerPlantLoad>(request.PowerPlants.Length);

            decimal minGeneration = request.PowerPlants.Sum(p => p.PMin);

            // Requested load below the minimum possible generation is an exceptional case, should be processed somehow in
            // production code. For now just take 0 in this case. Everyone will generate its minimum.
            decimal generateAboveMin = Math.Max(request.Load - minGeneration, 0m);

            foreach (PowerPlant plant in request.PowerPlants.OrderBy(p => PowerPlantMWhPrice(p, request.Fuels)))
            {
                var plantLoad = new PowerPlantLoad
                {
                    Name = plant.Name,
                    Power = plant.PMin
                };

                // what extra can a power plant generate above its minimum?
                decimal plantExtra = Math.Min(generateAboveMin, plant.PMax - plant.PMin);

                plantLoad.Power += plantExtra;
                generateAboveMin -= plantExtra;

                powerPlantLoads.Add(plantLoad);
            }

            return powerPlantLoads.ToArray();
        }

        /// <summary>
        /// Calculetes price per MWh of power plant's _output_ energy taking in account its fuel price and efficiency.
        /// </summary>
        private static decimal PowerPlantMWhPrice(PowerPlant plant, Fuels fuels)
        {
            decimal fuelPrice = plant.Type switch
            {
                "gasfired" => fuels.GasPrice,
                "turbojet" => fuels.KerosinePrice,
                "windturbine" => 0m, // What is the weather? Windturbines are not always available.
                _ => throw new ArgumentException("Unknown power plant type."),
            };

            // TODO: add CO2 price or whatever else to the formula.
            return fuelPrice / plant.Efficiency;
        }
    }
}
