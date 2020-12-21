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
        private IRepository<ReportDAL> _repository;

        private IStaffService _staffService;
        private ITasksService _tasksService;

        private int _sprintReportId = -1;
        
        public ReportService(IRepository<ReportDAL> repository, IStaffService staffService, ITasksService tasksService)
        {
            _staffService = staffService;
            _tasksService = tasksService;
            
            _repository = repository;
        }

        public void FixReportDTO(ReportDTO reportDTO)
        {
            var reportDAL = reportDTO.ToReportDAL();
            _repository.Fix(reportDAL);
            reportDTO.Id = reportDAL.Id;
        }

        public int CreateDailyReport(StaffDTO staffDTO)
        {
            _staffService.FixStaffDTO(staffDTO);
            var reportDAL = new ReportDTO(-1, DateTime.Now, staffDTO.Id).ToReportDAL();
            _repository.Create(reportDAL);
            return reportDAL.Id;
        }

        public void UpdateDailyReport(ReportDTO reportDTO)
        {
            FixReportDTO(reportDTO);
            var report = new ReportDTO(_repository.Get(reportDTO.Id));
            var staff = _staffService.GetById(report.StaffId);
            report.ChangesTasksId = _tasksService.FindTasksModifiedByStaffAndDate(staff, DateTime.Now)
                .Select(taskDTO => taskDTO.Id).Distinct().ToList();
            
            foreach (var taskId in _tasksService.GetAllTasks().Where(t => t.State == TaskDTO.TaskState.Resolved && t.StaffId == staff.Id).Select(t => t.Id))
            {
                report.AddResolveTask(taskId);
            }
            
            _repository.Update(report.ToReportDAL());
        }

        public void MarkDailyReportFinalised(ReportDTO reportDTO)
        {
            FixReportDTO(reportDTO);
            var report = new ReportDTO(_repository.Get(reportDTO.Id)) {Finalised = true};
            _repository.Update(report.ToReportDAL());
        }

        public IEnumerable<ReportDTO> GetAllReports()
        {
            return _repository.GetAll().Select(t => new ReportDTO(t));
        }

        
        public int CreateSprintReport()
        {
            // Он не отличается от создания дневного, кроме того, что принадлежит директору == корню
            throw new NotImplementedException();
            /*_sprintReportId = CreateDailyReport(TODO);
            return _sprintReportId;*/
        }
        
        public void UpdateSprintReport()
        {
            // ToDO
            throw new NotImplementedException();
            /*
            // Вызовем UpdateDailyReport для всех отчетов
            foreach (var reportDTO in GetAllReports())
            {
                UpdateDailyReport(TODO);
            }
            
            // ToDO: abracadabra -> Projiekt Reparo!
            
            UpdateDailyReport(TODO);
            // ToDO: work with staff
            */
        }

        public ReportDTO GetById(int reportId)
        {
            return new ReportDTO(_repository.Get(reportId));
        }

        public IEnumerable<ReportDTO> FindReportsByStaffId(StaffDTO staffDTO)
        {
            _staffService.FixStaffDTO(staffDTO);
            return _repository.GetAll().Where(t => t.StaffId == staffDTO.Id).Select(t => new ReportDTO(t));
        }
    }
}