using System;

namespace Lab_6_DAL.Exceptions
{
    public class ReportIdNotFoundException : Exception
    {
        public ReportIdNotFoundException()
        {
        }

        public ReportIdNotFoundException(string message)
            : base(message)
        {
        }

        public ReportIdNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}