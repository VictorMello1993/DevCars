using DevCars.API.Persistence;
using DevCars.API.ViewModels;
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

        public GetAllCarsQueryHandler(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CarItemViewModel>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            var cars = _dbContext.Cars;

            var carsViewModel = await cars.Select(c => new CarItemViewModel(c.Id, c.Brand, c.Model, c.Price)).ToListAsync();

            return carsViewModel;            
        }
    }
}
