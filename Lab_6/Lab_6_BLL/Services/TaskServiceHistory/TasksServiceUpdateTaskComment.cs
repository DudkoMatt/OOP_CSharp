using System;

namespace Lab_6_BLL.Services.TaskServiceHistory
{
    public class TasksServiceUpdateTaskComment : TasksServiceModifyTask
    {
        public readonly string NewComment;

        public TasksServiceUpdateTaskComment(DateTime dateTime, int taskId, int staffId, string newComment) : base(dateTime, taskId, staffId)
        {
            NewComment = newComment;
        }
    }
}