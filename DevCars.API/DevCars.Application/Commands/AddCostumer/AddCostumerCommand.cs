using MediatR;
using System;

namespace DevCars.Application.Commands.AddCostumer
{
    public class AddCostumerCommand : IRequest<int>
    {
        //public AddCostumerInputModel(string fullName, string document, DateTime birthDate)
        //{
        //    FullName = fullName;
        //    Document = document;
        //    BirthDate = birthDate;
        //}

        public string FullName { get; set; }
        public string Document { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
