using DevCars.API.Entities;
using DevCars.API.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevCars.Application.Commands.AddOrder
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, int>
    {
        private readonly DevCarsDbContext _dbContext;

        public AddOrderCommandHandler(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == request.IdCar);
            var extraItems = request.ExtraItems.Select(e => new ExtraOrderItem(e.Description, e.Price)).ToList();

            if (car != null)
            {
                var order = new Order(request.IdCar, request.IdCostumer, car.Price, extraItems);

                car.SetAsSold();

                await _dbContext.Orders.AddAsync(order);
                await _dbContext.SaveChangesAsync();

                return order.Id;
            }

            return default(int);
        }
    }
}
