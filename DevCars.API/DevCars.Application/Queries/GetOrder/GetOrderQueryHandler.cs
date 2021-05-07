using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevCars.Application.Queries.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDetailsViewModel>
    {
        private readonly DevCarsDbContext _dbContext;

        public GetOrderQueryHandler(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderDetailsViewModel> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.Include(o => o.ExtraItems)
                                               .Include(c => c.Customer)
                                               .SingleOrDefaultAsync(o => o.Id == request.OrderId);

            if(order == null)
            {
                return null;
            }

            var extraItems = order.ExtraItems.Select(e => e.Description).ToList();

            var orderViewModel = new OrderDetailsViewModel(order.IdCar, order.IdCostumer, order.TotalCost, extraItems);

            return orderViewModel;
        }
    }
}
