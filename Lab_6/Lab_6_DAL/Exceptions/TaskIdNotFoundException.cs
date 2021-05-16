using System;

namespace Lab_6_DAL.Exceptions
{
    public class TaskIdNotFoundException : Exception
    {
        public TaskIdNotFoundException()
        {
        }

        public TaskIdNotFoundException(string message)
            : base(message)
        {
        }

        public TaskIdNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}