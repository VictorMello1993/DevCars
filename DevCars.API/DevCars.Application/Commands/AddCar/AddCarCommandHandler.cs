using DevCars.API.Entities;
using DevCars.API.Persistence;
using DevCars.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevCars.Application.Commands.AddCar
{
    public class AddCarCommandHandler : IRequestHandler<AddCarCommand, int>
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly ICarRepository _carRepository;

        public AddCarCommandHandler(DevCarsDbContext dbContext, ICarRepository carRepository)
        {
            _dbContext = dbContext;
            _carRepository = carRepository;
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

            await _carRepository.AddAsync(car);

            return car.Id;
        }
    }
}
