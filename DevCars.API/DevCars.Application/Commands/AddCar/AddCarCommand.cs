using MediatR;
using System;

namespace DevCars.Application.Commands.AddCar
{
    public class AddCarCommand : IRequest<int>
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public DateTime ProductionDate { get; set; }

        //Chassi do carro
        public string VinCode { get; set; }
    }
}
