using System;

namespace Lab_5
{
    public class AccountIdNotFoundException : Exception
    {
        public AccountIdNotFoundException()
        {
        }

        public AccountIdNotFoundException(string message)
            : base(message)
        {
        }

        public AccountIdNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}