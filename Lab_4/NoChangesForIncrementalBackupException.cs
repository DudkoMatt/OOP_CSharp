using System;

namespace Lab_4
{
    public class NoChangesForIncrementalBackupException : Exception
    {
        public NoChangesForIncrementalBackupException()
        {
        }

        public NoChangesForIncrementalBackupException(string message)
            : base(message)
        {
        }

        public NoChangesForIncrementalBackupException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}