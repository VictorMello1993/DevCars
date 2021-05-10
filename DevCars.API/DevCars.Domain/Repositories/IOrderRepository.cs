using DevCars.API.Entities;
using System.Threading.Tasks;

namespace DevCars.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderAsync(int orderId);
        Task<Order> GetOrderAsync(int customerId, int orderId);
        Task AddAsync(Order order);
    }
}
