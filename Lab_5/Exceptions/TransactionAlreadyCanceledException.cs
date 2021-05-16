using System;

namespace Lab_5
{
    public class TransactionAlreadyCanceledException : Exception
    {
        public TransactionAlreadyCanceledException()
        {
        }

        public TransactionAlreadyCanceledException(string message)
            : base(message)
        {
        }

        public TransactionAlreadyCanceledException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}