using System;
using Lab_6_BLL.DTO;

namespace Lab_6_BLL.Services.TaskServiceHistory
{
    public class TasksServiceUpdateTaskState : TasksServiceModifyTask
    {
        public readonly TaskDTO.TaskState NewState;

        public TasksServiceUpdateTaskState(DateTime dateTime, int taskId, int staffId, TaskDTO.TaskState newState) : base(dateTime, taskId, staffId)
        {
            NewState = newState;
        }
    }
}