using System;

namespace Lab_5
{
    public class DepositAccountNotExpiredYetException : Exception
    {
        public DepositAccountNotExpiredYetException()
        {
        }

        public DepositAccountNotExpiredYetException(string message)
            : base(message)
        {
        }

        public DepositAccountNotExpiredYetException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}