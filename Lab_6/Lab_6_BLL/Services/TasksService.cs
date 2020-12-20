using System;
using System.Collections.Generic;
using System.Linq;
using Lab_6_BLL.DTO;
using Lab_6_BLL.Infrastructure;
using Lab_6_BLL.Services.TaskServiceHistory;
using Lab_6_DAL.Entities;
using Lab_6_DAL.Infrastructure;

namespace Lab_6_BLL.Services
{
    public class TasksService : ITasksService
    {
        private int _nextTaskId;
        private int _nextHistoryChangeId;

        private IRepository<TaskDAL> _repository;

        private readonly Dictionary<int, TasksServiceHistoryEntry> _history;
        
        public TasksService(IRepository<TaskDAL> repository)
        {
            _history = new Dictionary<int, TasksServiceHistoryEntry>();
            _repository = repository;
            _nextTaskId = repository.GetMaxId() + 1;
        }
        
        private void AddHistoryEntry(TasksServiceHistoryEntry entry)
        {
            _history.Add(_nextHistoryChangeId++, entry);
        }
        
        public int CreateTask(string name, string description, int staffId)
        {
            var task = new TaskDTO(_nextTaskId, name, description, TaskDTO.TaskState.Active, staffId);
            
            _repository.Create(task.ToTaskDAL());
            AddHistoryEntry(new TasksServiceCreateTask(DateTime.Now, _nextTaskId, staffId));
            return _nextTaskId++;
        }

        private void UpdateTaskState(int taskId, TaskDTO.TaskState state)
        {
            var temp = new TaskDTO(_repository.Get(taskId)) {State = state};
            _repository.Update(temp.ToTaskDAL());
            AddHistoryEntry(new TasksServiceUpdateTaskState(DateTime.Now, taskId, temp.StaffId, state));
        }
        
        public void MarkTaskAsResolved(int taskId)
        {
            UpdateTaskState(taskId, TaskDTO.TaskState.Resolved);
        }

        public void MarkTaskAsOpen(int taskId)
        {
            UpdateTaskState(taskId, TaskDTO.TaskState.Open);
        }

        public void MarkTaskAsActive(int taskId)
        {
            UpdateTaskState(taskId, TaskDTO.TaskState.Active);
        }

        public void UpdateStaffIdInTask(int taskId, int staffId)
        {
            var temp = new TaskDTO(_repository.Get(taskId)) {StaffId = staffId};
            _repository.Update(temp.ToTaskDAL());
            AddHistoryEntry(new TasksServiceUpdateTaskStaffId(DateTime.Now, taskId, temp.StaffId, staffId));
        }

        public void UpdateTaskComment(int taskId, string comment)
        {
            var temp = new TaskDTO(_repository.Get(taskId)) {Comment = comment};
            _repository.Update(temp.ToTaskDAL());
            AddHistoryEntry(new TasksServiceUpdateTaskComment(DateTime.Now, taskId, temp.StaffId, comment));
        }

        public IEnumerable<TaskDTO> GetAllTasks()
        {
            return _repository.GetAll().Select(t => new TaskDTO(t));
        }

        public TaskDTO GetTaskById(int taskId)
        {
            return new TaskDTO(_repository.Get(taskId));
        }
        
        public IEnumerable<TaskDTO> FindTasksByCreationDate(DateTime date)
        {
            var tasks = new List<TaskDTO>();
            foreach (var (_, historyEntry) in _history)
            {
                if (historyEntry.DateTime.Date < date.Date)
                    break;

                if (!(historyEntry is TasksServiceCreateTask createTask)) continue;
                if (createTask.DateTime.Date == date.Date)
                    tasks.Add( new TaskDTO(_repository.Get(createTask.TaskId)));
            }

            return tasks;
        }

        public IEnumerable<TaskDTO> FindTasksByModificationDate(DateTime date)
        {
            var tasks = new List<TaskDTO>();
            foreach (var (_, historyEntry) in _history)
            {
                if (historyEntry.DateTime.Date < date.Date)
                    break;

                if (!(historyEntry is TasksServiceModifyTask modifyTask)) continue;
                if (modifyTask.DateTime.Date == date.Date)
                    tasks.Add(new TaskDTO(_repository.Get(modifyTask.TaskId)));
            }

            return tasks;
        }

        public IEnumerable<TaskDTO> FindTasksByStaffId(int staffId)
        {
            return GetAllTasks().Where(t => t.StaffId == staffId).ToList();
        }

        public IEnumerable<TaskDTO> FindTasksModifiedByStaffId(int staffId)
        {
            var tasks = new List<TaskDTO>();
            foreach (var (_, historyEntry) in _history)
            {
                if (!(historyEntry is TasksServiceModifyTask modifyTask)) continue;
                if (modifyTask.StaffId == staffId)
                    tasks.Add(new TaskDTO(_repository.Get(modifyTask.TaskId)));
            }

            return tasks;
        }

        public IEnumerable<TaskDTO> FindTasksByMentor(StaffDTO mentor)
        {
            var tasks = new List<TaskDTO>();
            foreach (var staffId in mentor.SubordinatesId)
            {
                tasks.AddRange(FindTasksByStaffId(staffId));
            }

            return tasks;
        }

        public IEnumerable<TaskDTO> FindTasksModifiedByStaffIdAndDate(int staffId, DateTime date)
        {
            var tasks = new List<TaskDTO>();
            foreach (var (_, historyEntry) in _history)
            {
                if (!(historyEntry is TasksServiceModifyTask modifyTask)) continue;
                if (modifyTask.StaffId == staffId && historyEntry.DateTime.Date == date.Date)
                    tasks.Add(new TaskDTO(_repository.Get(modifyTask.TaskId)));
            }

            return tasks;
        }
    }
}