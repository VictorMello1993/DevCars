using DevCars.API.Entities;
using System.Threading.Tasks;

namespace DevCars.Domain.Repositories
{
    public interface ICostumerRepository
    {        
        Task AddAsync(Costumer costumer);        
    }
}
