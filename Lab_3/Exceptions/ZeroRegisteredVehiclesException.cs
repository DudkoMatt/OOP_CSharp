using System;

namespace Lab_3
{
    public class ZeroRegisteredVehiclesException : Exception
    {
        public ZeroRegisteredVehiclesException()
        {
        }

        public ZeroRegisteredVehiclesException(string message)
            : base(message)
        {
        }

        public ZeroRegisteredVehiclesException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}