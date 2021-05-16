using System;

namespace Lab_6_BLL.Services.TaskServiceHistory
{
    public class TasksServiceUpdateTaskStaffId : TasksServiceModifyTask
    {
        public readonly int NewStaffId;

        public TasksServiceUpdateTaskStaffId(DateTime dateTime, int taskId, int staffId, int newStaffId) : base(dateTime, taskId, staffId)
        {
            NewStaffId = newStaffId;
        }
    }
}