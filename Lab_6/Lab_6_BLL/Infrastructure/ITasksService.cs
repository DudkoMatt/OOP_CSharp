using System;
using System.Collections.Generic;
using Lab_6_BLL.DTO;

namespace Lab_6_BLL.Infrastructure
{
    public interface ITasksService
    {
        public int CreateTask(string name, string description, int staffId);
        public void MarkTaskAsResolved(int taskId);
        public void MarkTaskAsOpen(int taskId);
        public void MarkTaskAsActive(int taskId);
        public void UpdateStaffIdInTask(int taskId, int staffId);
        public void UpdateTaskComment(int taskId, string comment);
        public IEnumerable<TaskDTO> GetAllTasks();
        public TaskDTO GetTaskById(int taskId);
        public IEnumerable<TaskDTO> FindTasksByCreationDate(DateTime time);
        public IEnumerable<TaskDTO> FindTasksByModificationDate(DateTime time);
        public IEnumerable<TaskDTO> FindTasksByStaffId(int staffId);
        public IEnumerable<TaskDTO> FindTasksModifiedByStaffId(int staffId);
        public IEnumerable<TaskDTO> FindTasksModifiedByStaffIdAndDate(int staffId, DateTime date);
        public IEnumerable<TaskDTO> FindTasksByMentor(StaffDTO mentor);
    }
}
