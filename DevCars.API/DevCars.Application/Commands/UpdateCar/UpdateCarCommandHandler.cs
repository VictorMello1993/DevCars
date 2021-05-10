using DevCars.API.Persistence;
using DevCars.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DevCars.Application.Commands.UpdateCar
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, Unit>
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly ICarRepository _carRepository;

        public UpdateCarCommandHandler(DevCarsDbContext dbContext, ICarRepository carRepository)
        {
            _dbContext = dbContext;
            _carRepository = carRepository;
        }

        public async Task<Unit> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _dbContext.Cars.SingleOrDefaultAsync(c => c.Id == request.Id);

            car.Update(request.Color, request.Price);

            await _carRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
