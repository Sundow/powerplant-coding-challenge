using Microsoft.AspNetCore.Mvc;

namespace PowerPlants.Model
{
    public class PowerPlantsResponse
    {
        public string Name { get; set; }

        [ModelBinder(Name = "p")]
        public decimal Power { get; set; }
    }
}
