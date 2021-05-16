using System;

namespace Lab_5
{
    public class ClientIdNotFoundException : Exception
    {
        public ClientIdNotFoundException()
        {
        }

        public ClientIdNotFoundException(string message)
            : base(message)
        {
        }

        public ClientIdNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}