using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using DevCars.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevCars.Application.Queries.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDetailsViewModel>
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly IOrderRepository _orderRepository;

        public GetOrderQueryHandler(DevCarsDbContext dbContext, IOrderRepository orderRepository)
        {
            _dbContext = dbContext;
            _orderRepository = orderRepository;
        }

        public async Task<OrderDetailsViewModel> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderAsync(request.CostumerId, request.OrderId);

            if(order == null)
            {
                return null;
            }

            var extraItems = order.ExtraItems != null && order.ExtraItems.Count > 0 ? 
                             order.ExtraItems.Select(e => e.Description).ToList() : null;

            var orderViewModel = new OrderDetailsViewModel(order.IdCar, order.IdCostumer, order.TotalCost, extraItems);

            return orderViewModel;
        }
    }
}
