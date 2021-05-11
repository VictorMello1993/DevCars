using MediatR;
using System.Collections.Generic;

namespace DevCars.Application.Commands.AddOrder
{
    public class AddOrderCommand : IRequest<int>
    {
        public int IdCar { get; set; }
        public int IdCostumer { get; set; }
        public List<ExtraItemInputModel> ExtraItems { get; set; }

        public class ExtraItemInputModel
        {
            public string Description { get; set; }
            public decimal Price { get; set; }
        }
    }
}
