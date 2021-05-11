using DevCars.API.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCars.Application.Queries.GetAllCars
{
    public class GetAllCarsQuery : IRequest<List<CarItemViewModel>>
    {

    }
}
