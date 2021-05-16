using System;
using System.Collections.Generic;
using Lab_6_BLL.DTO;

namespace Lab_6_BLL.Infrastructure
{
    public interface ITasksService
    {
        public int CreateTask(TaskDTO taskDTO, StaffDTO staffDTO);
        public void MarkTaskAsResolved(TaskDTO taskDTO);
        public void MarkTaskAsOpen(TaskDTO taskDTO);
        public void MarkTaskAsActive(TaskDTO taskDTO);
        public void UpdateStaffIdInTask(TaskDTO taskDTO, StaffDTO staffDTO);
        public void UpdateTaskComment(TaskDTO oldTaskDTO, TaskDTO newTaskDTO);
        public IEnumerable<TaskDTO> GetAllTasks();
        public TaskDTO GetTaskById(TaskDTO taskDTO);
        public TaskDTO GetById(int taskId);
        public IEnumerable<TaskDTO> FindTasksByCreationDate(DateTime time);
        public IEnumerable<TaskDTO> FindTasksByModificationDate(DateTime time);
        public IEnumerable<TaskDTO> FindTasksByStaff(StaffDTO staffDTO);
        public IEnumerable<TaskDTO> FindTasksModifiedByStaff(StaffDTO staffDTO);
        public IEnumerable<TaskDTO> FindTasksModifiedByStaffAndDate(StaffDTO staffDTO, DateTime date);
        public IEnumerable<TaskDTO> FindTasksByMentor(StaffDTO mentor);
        public void FixTaskDTO(TaskDTO taskDTO);
    }
}
