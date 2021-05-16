using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private int _nextHistoryChangeId;

        private IRepository<TaskDAL> _repository;

        private readonly Dictionary<int, TasksServiceHistoryEntry> _history;
        private WeakReference<IStaffService> _staffService;
        
        public TasksService(IRepository<TaskDAL> repository, IStaffService staffService)
        {
            _history = new Dictionary<int, TasksServiceHistoryEntry>();
            _repository = repository;
            _staffService = new WeakReference<IStaffService>(staffService);
        }

        public void FixTaskDTO(TaskDTO taskDTO)
        {
            var taskDAL = taskDTO.ToTaskDAL();
            _repository.Fix(taskDAL);
            taskDTO.Id = taskDAL.Id;
        }
        
        private void AddHistoryEntry(TasksServiceHistoryEntry entry)
        {
            _history.Add(_nextHistoryChangeId++, entry);
        }
        
        public int CreateTask(TaskDTO taskDTO, StaffDTO staffDTO)
        {
            _staffService.TryGetTarget(out var staffService);
            staffService.FixStaffDTO(staffDTO);
            
            var taskDAL = taskDTO.ToTaskDAL();
            taskDAL.StaffId = staffDTO.Id;
            
            _repository.Create(taskDAL);
            taskDTO.Id = taskDAL.Id;

            AddHistoryEntry(new TasksServiceCreateTask(DateTime.Now, taskDTO.Id, staffDTO.Id));
            return taskDTO.Id;
        }
        
        private void UpdateTaskState(TaskDTO taskDTO, TaskDTO.TaskState state)
        {
            var temp = new TaskDTO(_repository.Get(taskDTO.Id)) {State = state};
            _repository.Update(temp.ToTaskDAL());
            AddHistoryEntry(new TasksServiceUpdateTaskState(DateTime.Now, taskDTO.Id, temp.StaffId, state));
        }

        public void MarkTaskAsResolved(TaskDTO taskDTO)
        {
            FixTaskDTO(taskDTO);
            UpdateTaskState(taskDTO, TaskDTO.TaskState.Resolved);
        }

        public void MarkTaskAsOpen(TaskDTO taskDTO)
        {
            FixTaskDTO(taskDTO);
            UpdateTaskState(taskDTO, TaskDTO.TaskState.Open);
        }

        public void MarkTaskAsActive(TaskDTO taskDTO)
        {
            FixTaskDTO(taskDTO);
            UpdateTaskState(taskDTO, TaskDTO.TaskState.Active);
        }
        
        public void UpdateStaffIdInTask(TaskDTO taskDTO, StaffDTO staffDTO)
        {
            FixTaskDTO(taskDTO);
            var temp = new TaskDTO(_repository.Get(taskDTO.Id)) {StaffId = staffDTO.Id};
            _repository.Update(temp.ToTaskDAL());
            taskDTO.StaffId = staffDTO.Id;
            AddHistoryEntry(new TasksServiceUpdateTaskStaffId(DateTime.Now, taskDTO.Id, temp.StaffId, staffDTO.Id));
        }

        public void UpdateTaskComment(TaskDTO oldTaskDTO, TaskDTO newTaskDTO)
        {
            FixTaskDTO(oldTaskDTO);
            var temp = new TaskDTO(_repository.Get(oldTaskDTO.Id)) {Comment = newTaskDTO.Comment};
            _repository.Update(temp.ToTaskDAL());
            oldTaskDTO.Comment = newTaskDTO.Comment;
            AddHistoryEntry(new TasksServiceUpdateTaskComment(DateTime.Now, oldTaskDTO.Id, temp.StaffId, newTaskDTO.Comment));
        }

        public IEnumerable<TaskDTO> GetAllTasks()
        {
            return _repository.GetAll().Select(t => new TaskDTO(t));
        }

        public TaskDTO GetTaskById(TaskDTO taskDTO)
        {
            FixTaskDTO(taskDTO);
            return new TaskDTO(_repository.Get(taskDTO.Id));
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

        public IEnumerable<TaskDTO> FindTasksByStaff(StaffDTO staffDTO)
        {
            return GetAllTasks().Where(t => t.StaffId == staffDTO.Id).ToList();
        }

        public IEnumerable<TaskDTO> FindTasksModifiedByStaff(StaffDTO staffDTO)
        {
            var tasks = new List<TaskDTO>();
            foreach (var (_, historyEntry) in _history)
            {
                if (!(historyEntry is TasksServiceModifyTask modifyTask)) continue;
                if (modifyTask.StaffId == staffDTO.Id)
                    tasks.Add(new TaskDTO(_repository.Get(modifyTask.TaskId)));
            }

            return tasks;
        }

        public IEnumerable<TaskDTO> FindTasksByMentor(StaffDTO mentor)
        {
            var tasks = new List<TaskDTO>();
            foreach (var staffId in mentor.SubordinatesId)
            {
                _staffService.TryGetTarget(out var staffService);
                tasks.AddRange(FindTasksByStaff(staffService.GetById(staffId)));
            }

            return tasks;
        }

        public IEnumerable<TaskDTO> FindTasksModifiedByStaffAndDate(StaffDTO staffDTO, DateTime date)
        {
            var tasks = new List<TaskDTO>();
            foreach (var (_, historyEntry) in _history)
            {
                if (!(historyEntry is TasksServiceModifyTask modifyTask)) continue;
                if (modifyTask.StaffId == staffDTO.Id && historyEntry.DateTime.Date == date.Date)
                    tasks.Add(new TaskDTO(_repository.Get(modifyTask.TaskId)));
            }

            return tasks;
        }

        public TaskDTO GetById(int taskId)
        {
            return new TaskDTO(_repository.Get(taskId));
        }
    }
}