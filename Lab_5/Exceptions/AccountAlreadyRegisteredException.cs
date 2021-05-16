using System;

namespace Lab_5
{
    public class AccountAlreadyRegisteredException : Exception
    {
        public AccountAlreadyRegisteredException()
        {
        }

        public AccountAlreadyRegisteredException(string message)
            : base(message)
        {
        }

        public AccountAlreadyRegisteredException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}