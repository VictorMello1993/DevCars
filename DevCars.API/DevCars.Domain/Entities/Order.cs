using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class Order
    {
        public Order(int idCar, int idCostumer, decimal price, List<ExtraOrderItem> items)
        {            
            IdCar = idCar;
            IdCostumer = idCostumer;
            TotalCost = items.Sum(i => i.Price) + price;

            ExtraItems = items;
        }

        protected Order() { }

        public int Id { get; private set; }
        public int IdCar { get; private set; }
        public Car Car { get; private set; }        
        public int IdCostumer { get; private set; }
        public Costumer Customer { get; private set; }
        public decimal TotalCost { get; private set; }
        public List<ExtraOrderItem> ExtraItems { get; set; }
    }

    public class ExtraOrderItem
    {
        public ExtraOrderItem(string description, decimal price)
        {
            Description = description;
            Price = price;
        }

        protected ExtraOrderItem() { }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int IdOrder { get; private set; }        
    }
}
