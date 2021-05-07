using DevCars.API.Entities;
using DevCars.API.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevCars.Application.Commands.AddCostumer
{
    public class AddCostumerCommandHandler : IRequestHandler<AddCostumerCommand, int>
    {
        private readonly DevCarsDbContext _dbContext;

        public AddCostumerCommandHandler(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddCostumerCommand request, CancellationToken cancellationToken)
        {
            var costumer = new Customer(request.FullName, request.Document, request.BirthDate);

            await _dbContext.Customers.AddAsync(costumer);
            await _dbContext.SaveChangesAsync();

            return costumer.Id;
        }
    }
}
