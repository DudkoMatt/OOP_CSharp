using System;

namespace Lab_5
{
    public class SuspiciousAccountLimitException : Exception
    {
        public SuspiciousAccountLimitException()
        {
        }

        public SuspiciousAccountLimitException(string message)
            : base(message)
        {
        }

        public SuspiciousAccountLimitException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}