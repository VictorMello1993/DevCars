using DevCars.API.Entities;
using DevCars.API.Persistence;
using DevCars.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace DevCars.Infrastructure.Persistence.Repositories
{
    public class CostumerRepository : ICostumerRepository
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly string _connectionString;

        public CostumerRepository(DevCarsDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevCarsConnectionString");
        }

        public async Task AddAsync(Costumer costumer)
        {
            await _dbContext.AddAsync(costumer);
            await _dbContext.SaveChangesAsync();
        }        
    }
}
