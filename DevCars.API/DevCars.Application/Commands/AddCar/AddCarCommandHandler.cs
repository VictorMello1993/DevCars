using DevCars.API.Entities;
using DevCars.API.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevCars.Application.Commands.AddCar
{
    public class AddCarCommandHandler : IRequestHandler<AddCarCommand, int>
    {
        private readonly DevCarsDbContext _dbContext;

        public AddCarCommandHandler(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            var car = new Car(request.VinCode,
                              request.Brand,
                              request.Model,
                              request.Year,
                              request.Price,
                              request.Color,
                              request.ProductionDate);

            await _dbContext.Cars.AddAsync(car);
            await _dbContext.SaveChangesAsync();

            return car.Id;
        }
    }
}
