using MediatR;

namespace DevCars.Application.Commands.UpdateCar
{
    public class UpdateCarCommand : IRequest<Unit>
    {
        public UpdateCarCommand(string color, decimal price, int id)
        {
            Color = color;
            Price = price;
            Id = id;
        }

        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
    }
}
