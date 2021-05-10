using System;

namespace DevCars.Domain.Exceptions
{
    public class OnlyAvailableCarsCanBeSoldException : Exception
    {
        public OnlyAvailableCarsCanBeSoldException() : base("Only available cars can be sold.")
        {

        }
    }
}
