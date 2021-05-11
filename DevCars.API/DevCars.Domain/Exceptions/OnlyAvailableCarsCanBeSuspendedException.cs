using System;

namespace DevCars.Domain.Exceptions
{
    public class OnlyAvailableCarsCanBeSuspendedException : Exception
    {
        public OnlyAvailableCarsCanBeSuspendedException() : base("Only available cars can be suspended.")
        {

        }
    }
}
