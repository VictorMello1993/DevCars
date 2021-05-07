using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DevCars.Application.Queries.GetCarById
{
    public class GetByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarDetailsViewModel>
    {
        private readonly DevCarsDbContext _dbContext;

        public GetByIdQueryHandler(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CarDetailsViewModel> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var car = await _dbContext.Cars.SingleOrDefaultAsync(c => c.Id == request.Id);

            if (car == null)
            {
                return null;
            }

            var carDetailsViewModel = new CarDetailsViewModel(car.Id, car.Brand, car.Model, car.Color, car.Year, car.Price, car.ProductionDate, car.VinCode);

            return carDetailsViewModel;
        }
    }
}
