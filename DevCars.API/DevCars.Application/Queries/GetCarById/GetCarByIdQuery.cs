using DevCars.API.ViewModels;
using MediatR;
using System;

namespace DevCars.Application.Queries.GetCarById
{
    public class GetCarByIdQuery : IRequest<CarDetailsViewModel>
    {
        public GetCarByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
