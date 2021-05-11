using DevCars.API.Entities;
using DevCars.API.Persistence;
using DevCars.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevCars.Infrastructure.Persistence.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly string _connectionString;

        public CarRepository(DevCarsDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevCarsConnectionString");
        }

        public async Task AddAsync(Car car)
        {
            await _dbContext.AddAsync(car);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _dbContext.Cars.ToListAsync();
        }

        public async Task<Car> GetCarById(int id)
        {
            return await _dbContext.Cars.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
