using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using DevCars.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevCars.Application.Queries.GetCarById
{
    public class GetByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarDetailsViewModel>
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly ICarRepository _carRepository;

        public GetByIdQueryHandler(DevCarsDbContext dbContext, ICarRepository carRepository)
        {
            _dbContext = dbContext;
            _carRepository = carRepository;
        }

        public async Task<CarDetailsViewModel> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetCarById(request.Id);

            if (car == null)
            {
                return null;
            }

            var carDetailsViewModel = new CarDetailsViewModel(car.Id, car.Brand, car.Model, car.Color, car.Year, 
                                                              car.Price, car.ProductionDate, car.VinCode);

            return carDetailsViewModel;
        }
    }
}
