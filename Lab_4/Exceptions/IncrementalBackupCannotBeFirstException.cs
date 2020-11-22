using System;

namespace Lab_4
{
    public class IncrementalBackupCannotBeFirstException : Exception
    {
        public IncrementalBackupCannotBeFirstException()
        {
        }

        public IncrementalBackupCannotBeFirstException(string message)
            : base(message)
        {
        }

        public IncrementalBackupCannotBeFirstException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}