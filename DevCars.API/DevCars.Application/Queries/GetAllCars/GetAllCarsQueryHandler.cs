using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using DevCars.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevCars.Application.Queries.GetAllCars
{
    public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, List<CarItemViewModel>>
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly ICarRepository _carRepository;

        public GetAllCarsQueryHandler(DevCarsDbContext dbContext, ICarRepository carRepository)
        {
            _dbContext = dbContext;
            _carRepository = carRepository;
        }

        public async Task<List<CarItemViewModel>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            var cars = await _carRepository.GetAllCarsAsync();

            var carsViewModel = cars.Select(c => new CarItemViewModel(c.Id, c.Brand, c.Model, c.Price)).ToList();

            return carsViewModel;            
        }
    }
}
