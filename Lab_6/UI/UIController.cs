using System;
using System.Collections.Generic;
using System.Linq;
using Lab_6.UI.ViewModels;
using Lab_6_BLL.DTO;
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
            return _reportService.CreateDailyReport(staffVM.ToStaffDTO());
        }

        public void UpdateDailyReport(ReportVM reportVM)
        {
            _reportService.UpdateDailyReport(reportVM.ToReportDTO());
        }

        public void MarkDailyReportFinalised(ReportVM reportVM)
        {
            _reportService.MarkDailyReportFinalised(reportVM.ToReportDTO());
        }
        
        public IEnumerable<ReportVM> GetAllReports()
        {
            return _reportService.GetAllReports().Select(r => new ReportVM(r)).ToList();
        }

        public int CreateStaff(StaffVM staffVM)
        {
            return _staffService.CreateStaff(staffVM.ToStaffDTO());
        }

        public int CreateTask(TaskVM taskVM, StaffVM staffVM)
        {
            return _tasksService.CreateTask(taskVM.ToTaskDTO(), staffVM.ToStaffDTO());
        }

        public void MarkTaskAsResolved(TaskVM taskVM)
        {
            _tasksService.MarkTaskAsResolved(taskVM.ToTaskDTO());
        }

        public void UpdateTaskComment(TaskVM oldTaskVM, TaskVM newTaskVM)
        {
            _tasksService.UpdateTaskComment(oldTaskVM.ToTaskDTO(), newTaskVM.ToTaskDTO());
        }

        public IEnumerable<TaskVM> GetAllTasks()
        {
            return _tasksService.GetAllTasks().Select(t => new TaskVM(t)).ToList();
        }

        public StaffVM GetStaffById(int staffId)
        {
            return new StaffVM(_staffService.GetById(staffId));
        }
        
        public ReportVM GetReportById(int reportId)
        {
            return new ReportVM(_reportService.GetById(reportId));
        }
        
        public TaskVM GetTaskById(int taskId)
        {
            return new TaskVM(_tasksService.GetById(taskId));
        }

        public int CreateSprintReport()
        {
            return _reportService.CreateSprintReport();
        }

        public void UpdateSprintReport()
        {
            _reportService.UpdateSprintReport();
        }
    }
}