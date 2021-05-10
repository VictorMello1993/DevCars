using DevCars.API.Entities;
using DevCars.API.Persistence;
using DevCars.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevCars.Application.Commands.AddOrder
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, int>
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly IOrderRepository _orderRepository;

        public AddOrderCommandHandler(DevCarsDbContext dbContext, IOrderRepository orderRepository)
        {
            _dbContext = dbContext;
            _orderRepository = orderRepository;
        }

        public async Task<int> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == request.IdCar);
            var extraItems = request.ExtraItems.Select(e => new ExtraOrderItem(e.Description, e.Price)).ToList();

            if (car != null)
            {
                var order = new Order(request.IdCar, request.IdCostumer, car.Price, extraItems);

                car.SetAsSold();

                await _orderRepository.AddAsync(order);

                return order.Id;
            }

            return default(int);
        }
    }
}
