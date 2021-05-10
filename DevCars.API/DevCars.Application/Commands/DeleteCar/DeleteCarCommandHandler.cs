using DevCars.API.Persistence;
using DevCars.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DevCars.Application.Commands.DeleteCar
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, Unit>
    {
        private readonly DevCarsDbContext _dbContext;
        private ICarRepository _carRepository;

        public DeleteCarCommandHandler(DevCarsDbContext dbContext, ICarRepository carRepository)
        {
            _dbContext = dbContext;
            _carRepository = carRepository;
        }

        public async Task<Unit> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _dbContext.Cars.SingleOrDefaultAsync(c => c.Id == request.id);

            car.SetAsSuspended();

            await _carRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
