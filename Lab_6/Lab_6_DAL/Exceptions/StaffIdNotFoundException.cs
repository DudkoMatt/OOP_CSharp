using System;

namespace Lab_6_DAL.Exceptions
{
    public class StaffIdNotFoundException : Exception
    {
        public StaffIdNotFoundException()
        {
        }

        public StaffIdNotFoundException(string message)
            : base(message)
        {
        }

        public StaffIdNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}