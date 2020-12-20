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
        private readonly Dictionary<int, ReportDTO> _allReports;

        private IStaffService _staffService;
        private ITasksService _tasksService;

        private int _sprintReportId = -1;
        
        public ReportService(IRepository<ReportDAL> repository, IStaffService staffService, ITasksService tasksService)
        {
            _staffService = staffService;
            _tasksService = tasksService;
            
            _repository = repository;
            _nextReportId = repository.GetMaxId() + 1;

            _allReports = new Dictionary<int, ReportDTO>();
            for (var i = 0; i < repository.GetMaxId(); i++)
            {
                if (_repository.TryGet(i))
                    _allReports.Add(i, new ReportDTO(_repository.Get(i)));
            }
        }

        public int CreateDailyReport(int staffId)
        {
            var report = new ReportDTO(_nextReportId, DateTime.Now, staffId);
            _allReports.Add(_nextReportId, report);
            
            _repository.Create(report.ToReportDAL());
            
            return _nextReportId++;
        }

        public void UpdateDailyReport(int reportId)
        {
            var staffId = _allReports[reportId].StaffId;
            _allReports[reportId].ChangesTasksId = _tasksService.FindTasksModifiedByStaffIdAndDate(staffId, DateTime.Now)
                .Select(taskDTO => taskDTO.Id).ToList();
            
            foreach (var taskId in _tasksService.GetAllTasks().Where(t => t.State == TaskDTO.TaskState.Resolved && t.StaffId == staffId).Select(t => t.Id))
            {
                _allReports[reportId].AddResolveTask(taskId);
            }
        }

        public void MarkDailyReportFinalised(int reportId)
        {
            _allReports[reportId].Finalised = true;
        }

        public IEnumerable<ReportDTO> GetAllReports()
        {
            return _allReports.Values;
        }

        public int CreateSprintReport()
        {
            // Он не отличается от создания дневного, кроме того, что принадлежит директору == корню
            _sprintReportId = CreateDailyReport(0);
            return CreateDailyReport(0);
        }
        
        public void UpdateSprintReport()
        {
            // Вызовем UpdateDailyReport для всех отчетов
            foreach (var (_, reportDTO) in _allReports)
            {
                UpdateDailyReport(reportDTO.Id);
            }
            
            UpdateDailyReport(_sprintReportId);
        }

        public ReportDTO GetById(int reportId)
        {
            return _allReports[reportId];
        }
    }
}