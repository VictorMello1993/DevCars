using DevCars.API.ViewModels;
using MediatR;

namespace DevCars.Application.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<OrderDetailsViewModel>
    {
        public GetOrderQuery(int costumerId, int orderId)
        {
            CostumerId = costumerId;
            OrderId = orderId;
        }

        public int CostumerId { get; set; }
        public int OrderId { get; set; }
    }
}
