using System;

namespace Lab_5
{
    public class BankAlreadyRegisteredException : Exception
    {
        public BankAlreadyRegisteredException()
        {
        }

        public BankAlreadyRegisteredException(string message)
            : base(message)
        {
        }

        public BankAlreadyRegisteredException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}