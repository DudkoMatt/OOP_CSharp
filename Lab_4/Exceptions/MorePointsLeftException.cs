using System;

namespace Lab_4
{
    public class MorePointsLeftException : Exception
    {
        public MorePointsLeftException()
        {
        }

        public MorePointsLeftException(string message)
            : base(message)
        {
        }

        public MorePointsLeftException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}