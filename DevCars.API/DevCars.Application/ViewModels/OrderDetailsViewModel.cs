using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderDetailsViewModel(int idCar, int idCostumers, decimal totalCost, List<string> extraItems)
        {
            IdCar = idCar;
            IdCostumers = idCostumers;
            TotalCost = totalCost;
            ExtraItems = extraItems;
        }

        public int IdCar { get; set; }
        public int IdCostumers { get; set; }
        public decimal TotalCost { get; set; }
        public List<string> ExtraItems { get; set; }
    }
}
