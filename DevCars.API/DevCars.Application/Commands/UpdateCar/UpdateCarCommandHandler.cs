using DevCars.API.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DevCars.Application.Commands.UpdateCar
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, Unit>
    {
        private readonly DevCarsDbContext _dbContext;

        public UpdateCarCommandHandler(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _dbContext.Cars.SingleOrDefaultAsync(c => c.Id == request.Id);

            car.Update(request.Color, request.Price);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
