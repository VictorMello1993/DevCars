using System;

namespace DevCars.Domain.Exceptions
{
    public class CarAlreadySuspendedException : Exception
    {
        public CarAlreadySuspendedException() : base("The car is already suspended.")
        {

        }
    }
}
