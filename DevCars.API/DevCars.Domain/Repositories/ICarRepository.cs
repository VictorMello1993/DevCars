using DevCars.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevCars.Domain.Repositories
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllCarsAsync();
        Task<Car> GetCarById(int id);
        Task AddAsync(Car car);
        Task SaveChangesAsync();
    }
}
