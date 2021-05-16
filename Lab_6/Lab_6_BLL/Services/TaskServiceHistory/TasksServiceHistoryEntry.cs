using System;

namespace Lab_6_BLL.Services.TaskServiceHistory
{
    public abstract class TasksServiceHistoryEntry
    {
        public readonly DateTime DateTime;

        protected TasksServiceHistoryEntry(DateTime dateTime)
        {
            DateTime = dateTime;
        }
    }
}