using System;
using System.Collections.Generic;
using System.Linq;
using Lab_6.UI.ViewModels;
using Lab_6_BLL.Infrastructure;

namespace Lab_6.UI
{
    public class UIController
    {
        private IReportService _reportService;
        private IStaffService _staffService;
        private ITasksService _tasksService;

        public UIController(IReportService reportService, IStaffService staffService, ITasksService tasksService)
        {
            _reportService = reportService;
            _staffService = staffService;
            _tasksService = tasksService;
        }
        
        public int CreateDailyReport(StaffVM staffVM)
        {
            return _reportService.CreateDailyReport(staffVM.Id);
        }

        public void UpdateDailyReport(ReportVM reportVM)
        {
            _reportService.UpdateDailyReport(reportVM.Id);
        }

        public void MarkDailyReportFinalised(ReportVM reportVM)
        {
            _reportService.MarkDailyReportFinalised(reportVM.Id);
        }

        public ReportVM GetById(int reportId)
        {
            return new ReportVM(_reportService.GetById(reportId));
        }

        public IEnumerable<ReportVM> GetAllReports()
        {
            return _reportService.GetAllReports().Select(r => new ReportVM(r)).ToList();
        }

        public int CreateSprintReport()
        {
            return _reportService.CreateSprintReport();
        }

        public void UpdateSprintReport()
        {
            _reportService.UpdateSprintReport();
        }


        public int CreateStaff(string name)
        {
            return _staffService.CreateStaff(name);
        }

        public IEnumerable<StaffVM> GetAllStaff()
        {
            return _staffService.GetAllStaff().Select(s => new StaffVM(s)).ToList();
        }

        public void SetMentorId(StaffVM staffVM, StaffVM mentorVM)
        {
            _staffService.SetMentorId(staffVM.Id, mentorVM.Id);
        }

        public int GetMentorId(StaffVM staffVM)
        {
            return _staffService.GetMentorId(staffVM.Id);
        }

        public void AddSubordinate(StaffVM staffVM, StaffVM subordinateVM)
        {
            _staffService.AddSubordinate(staffVM.Id, subordinateVM.Id);
        }

        public void RemoveSubordinate(StaffVM staffVM, StaffVM subordinateVM)
        {
            _staffService.RemoveSubordinate(staffVM.Id, subordinateVM.Id);
        }


        public int CreateTask(string name, string description, StaffVM staffVM)
        {
            return _tasksService.CreateTask(name, description, staffVM.Id);
        }

        public void MarkTaskAsResolved(TaskVM taskVM)
        {
            _tasksService.MarkTaskAsResolved(taskVM.Id);
        }

        public void MarkTaskAsOpen(TaskVM taskVM)
        {
            _tasksService.MarkTaskAsOpen(taskVM.Id);
        }

        public void MarkTaskAsActive(TaskVM taskVM)
        {
            _tasksService.MarkTaskAsActive(taskVM.Id);
        }

        public void UpdateStaffIdInTask(TaskVM taskVM, StaffVM staffVM)
        {
            _tasksService.UpdateStaffIdInTask(taskVM.Id, staffVM.Id);
        }

        public void UpdateTaskComment(TaskVM taskVM)
        {
            _tasksService.UpdateTaskComment(taskVM.Id, taskVM.Comment);
        }

        public IEnumerable<TaskVM> GetAllTasks()
        {
            return _tasksService.GetAllTasks().Select(t => new TaskVM(t)).ToList();
        }

        public TaskVM GetTaskById(int taskId)
        {
            return new TaskVM(_tasksService.GetTaskById(taskId));
        }

        public IEnumerable<TaskVM> FindTasksByCreationDate(DateTime time)
        {
            return _tasksService.FindTasksByCreationDate(time).Select(t => new TaskVM(t)).ToList();
        }

        public IEnumerable<TaskVM> FindTasksByModificationDate(DateTime time)
        {
            return _tasksService.FindTasksByModificationDate(time).Select(t => new TaskVM(t)).ToList();
        }

        public IEnumerable<TaskVM> FindTasksByStaffId(StaffVM staffVM)
        {
            return _tasksService.FindTasksByStaffId(staffVM.Id).Select(t => new TaskVM(t)).ToList();
        }

        public IEnumerable<TaskVM> FindTasksModifiedByStaffId(StaffVM staffVM)
        {
            return _tasksService.FindTasksModifiedByStaffId(staffVM.Id).Select(t => new TaskVM(t)).ToList();
        }

        public IEnumerable<TaskVM> FindTasksModifiedByStaffIdAndDate(StaffVM staffVM, DateTime date)
        {
            return _tasksService.FindTasksModifiedByStaffIdAndDate(staffVM.Id, date).Select(t => new TaskVM(t)).ToList();
        }

        public IEnumerable<TaskVM> FindTasksByMentor(StaffVM mentorVM)
        {
            return _tasksService.FindTasksByMentor(mentorVM.ToStaffDTO()).Select(t => new TaskVM(t)).ToList();
        }
    }
}