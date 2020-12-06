using System;

namespace Lab_3
{
    public class VehicleTypeInvalidException : Exception
    {
        public VehicleTypeInvalidException()
        {
        }

        public VehicleTypeInvalidException(string message)
            : base(message)
        {
        }

        public VehicleTypeInvalidException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}