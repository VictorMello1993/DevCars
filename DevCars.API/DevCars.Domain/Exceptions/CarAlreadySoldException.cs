using System;

namespace DevCars.Domain.Exceptions
{
    public class CarAlreadySoldException : Exception
    {
        public CarAlreadySoldException() : base("The car is already sold.")
        {

        }
    }
}
