using Microsoft.AspNetCore.Mvc;

namespace PowerPlants.Model
{
    public class PowerPlantLoad
    {
        public string Name { get; set; }

        [ModelBinder(Name = "p")]
        public decimal Power { get; set; }
    }
}
