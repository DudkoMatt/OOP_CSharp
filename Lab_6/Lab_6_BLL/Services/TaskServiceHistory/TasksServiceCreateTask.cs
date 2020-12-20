using System;

namespace Lab_6_BLL.Services.TaskServiceHistory
{
    public class TasksServiceCreateTask : TasksServiceHistoryEntry
    {
        public readonly int TaskId;
        public readonly int StaffId;
        
        public TasksServiceCreateTask(DateTime dateTime, int taskId, int staffId) : base(dateTime)
        {
            TaskId = taskId;
            StaffId = staffId;
        }
    }
}