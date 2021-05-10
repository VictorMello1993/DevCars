using DevCars.API.Entities;
using DevCars.API.Persistence;
using DevCars.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DevCars.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly string _connectionString;

        public OrderRepository(DevCarsDbContext dbContext, IConfiguration connectionString)
        {
            _dbContext = dbContext;
            _connectionString = connectionString.GetConnectionString("DevCarsConnectionString");
        }

        public async Task AddAsync(Order order)
        {
            await _dbContext.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            return await _dbContext.Orders.Include(o => o.ExtraItems)
                                          .Include(c => c.Customer)
                                          .SingleOrDefaultAsync(o => o.Id == orderId);
        }
        
        public async Task<Order> GetOrderAsync(int costumerId, int orderId)
        {
            return await _dbContext.Orders.Include(o => o.ExtraItems).SingleOrDefaultAsync(o => o.Id == orderId && o.Customer.Id == costumerId);
        }
    }
}
