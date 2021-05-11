using DevCars.API.Entities;
using DevCars.API.Persistence;
using DevCars.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevCars.Application.Commands.AddCostumer
{
    public class AddCostumerCommandHandler : IRequestHandler<AddCostumerCommand, int>
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly ICostumerRepository _costumerRepository;

        public AddCostumerCommandHandler(DevCarsDbContext dbContext, ICostumerRepository costumerRepository)
        {
            _dbContext = dbContext;
            _costumerRepository = costumerRepository;
        }

        public async Task<int> Handle(AddCostumerCommand request, CancellationToken cancellationToken)
        {
            var costumer = new Costumer(request.FullName, request.Document, request.BirthDate);

            await _costumerRepository.AddAsync(costumer);

            return costumer.Id;
        }
    }
}
