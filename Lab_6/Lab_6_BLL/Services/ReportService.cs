using System;
using System.Collections.Generic;
using System.Linq;
using Lab_6_BLL.DTO;
using Lab_6_BLL.Infrastructure;
using Lab_6_DAL.Entities;
using Lab_6_DAL.Infrastructure;

namespace Lab_6_BLL.Services
{
    public class ReportService : IReportService
    {
        private int _nextReportId;

        private IRepository<ReportDAL> _repository;

        private IStaffService _staffService;
        private ITasksService _tasksService;

        private int _sprintReportId = -1;
        
        public ReportService(IRepository<ReportDAL> repository, IStaffService staffService, ITasksService tasksService)
        {
            _staffService = staffService;
            _tasksService = tasksService;
            
            _repository = repository;
            _nextReportId = repository.GetMaxId() + 1;
        }

        public int CreateDailyReport(int staffId)
        {
            var report = new ReportDTO(_nextReportId, DateTime.Now, staffId);
            _repository.Create(report.ToReportDAL());
            return _nextReportId++;
        }

        public void UpdateDailyReport(int reportId)
        {
            var report = new ReportDTO(_repository.Get(reportId));
            var staffId = report.StaffId;
            report.ChangesTasksId = _tasksService.FindTasksModifiedByStaffIdAndDate(staffId, DateTime.Now)
                .Select(taskDTO => taskDTO.Id).ToList();
            
            foreach (var taskId in _tasksService.GetAllTasks().Where(t => t.State == TaskDTO.TaskState.Resolved && t.StaffId == staffId).Select(t => t.Id))
            {
                report.AddResolveTask(taskId);
            }
            
            _repository.Update(report.ToReportDAL());
        }

        public void MarkDailyReportFinalised(int reportId)
        {
            var report = new ReportDTO(_repository.Get(reportId)) {Finalised = true};
            _repository.Update(report.ToReportDAL());
        }

        public IEnumerable<ReportDTO> GetAllReports()
        {
            return _repository.GetAll().Select(t => new ReportDTO(t));
        }

        
        public int CreateSprintReport()
        {
            // Он не отличается от создания дневного, кроме того, что принадлежит директору == корню
            _sprintReportId = CreateDailyReport(0);
            return _sprintReportId;
        }
        
        public void UpdateSprintReport()
        {
            // ToDO
            throw new NotImplementedException();
            // Вызовем UpdateDailyReport для всех отчетов
            foreach (var reportDTO in GetAllReports())
            {
                UpdateDailyReport(reportDTO.Id);
            }
            
            // ToDO: abracadabra -> Projiekt Reparo!
            
            UpdateDailyReport(_sprintReportId);
            // ToDO: work with staff
        }

        public ReportDTO GetById(int reportId)
        {
            return new ReportDTO(_repository.Get(reportId));
        }

        public IEnumerable<ReportDTO> FindReportsByStaffId(int staffId)
        {
            return _repository.GetAll().Where(t => t.StaffId == staffId).Select(t => new ReportDTO(t));
        }
    }
}