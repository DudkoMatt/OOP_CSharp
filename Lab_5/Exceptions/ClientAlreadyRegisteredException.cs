using System;

namespace Lab_5
{
    public class ClientAlreadyRegisteredException : Exception
    {
        public ClientAlreadyRegisteredException()
        {
        }

        public ClientAlreadyRegisteredException(string message)
            : base(message)
        {
        }

        public ClientAlreadyRegisteredException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}