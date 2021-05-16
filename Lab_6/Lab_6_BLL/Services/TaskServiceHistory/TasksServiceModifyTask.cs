using System;

namespace Lab_6_BLL.Services.TaskServiceHistory
{
    public abstract class TasksServiceModifyTask : TasksServiceHistoryEntry
    {
        public readonly int TaskId;
        public readonly int StaffId;

        protected TasksServiceModifyTask(DateTime dateTime, int taskId, int staffId) : base(dateTime)
        {
            TaskId = taskId;
            StaffId = staffId;
        }
    }
}