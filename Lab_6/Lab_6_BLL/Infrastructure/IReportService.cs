using System.Collections.Generic;
using Lab_6_BLL.DTO;

namespace Lab_6_BLL.Infrastructure
{
    public interface IReportService
    {
        public int CreateDailyReport(int staffId);
        public void UpdateDailyReport(int reportId);
        public void MarkDailyReportFinalised(int reportId);
        
        public ReportDTO GetById(int reportId);
        public IEnumerable<ReportDTO> GetAllReports();
        
        public int CreateSprintReport();
        public void UpdateSprintReport();
    }
}