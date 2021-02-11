using Microsoft.AspNetCore.Mvc;

namespace PowerPlants.Model
{
    public class Fuels
    {
        /// <summary>
        /// Gas price eur/MWh.
        /// </summary>
        [ModelBinder(Name = "gas(euro/MWh)")]
        public decimal GasPrice { get; set; }

        /// <summary>
        /// Kerosine price eur/MWh.
        /// </summary>
        [ModelBinder(Name = "kerosine(euro/MWh)")]
        public decimal KerosinePrice { get; set; }

        /// <summary>
        /// CO2 price eur/ton.
        /// </summary>
        [ModelBinder(Name = "co2(euro/ton)")]
        public decimal CO2Price { get; set; }

        /// <summary>
        /// Wind availability (?).
        /// </summary>
        [ModelBinder(Name = "wind(%)")]
        public decimal WindPerc { get; set; }
    }

    public class PowerPlant
    {
        public string Name { get; set; }

        // TODO: map to enum.
        public string Type { get; set; }

        public decimal Efficiency { get; set; }

        public decimal PMin { get; set; }

        public decimal PMax { get; set; }
    }

    public class PowerPlantsRequest
    {
        /// <summary>
        /// Requested load (MW).
        /// </summary>
        public decimal Load { get; set; }

        public Fuels Fuels { get; set; }

        public PowerPlant[] PowerPlants { get; set; }
    }
}
