using DevCars.API.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DevCars.Application.Commands.DeleteCar
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, Unit>
    {
        private readonly DevCarsDbContext _dbContext;

        public DeleteCarCommandHandler(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _dbContext.Cars.SingleOrDefaultAsync(c => c.Id == request.id);

            car.SetAsSuspended();

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
