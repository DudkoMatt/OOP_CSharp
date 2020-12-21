using System.Collections.Generic;
using Lab_6_BLL.DTO;

namespace Lab_6_BLL.Infrastructure
{
    public interface IReportService
    {
        public int CreateDailyReport(StaffDTO staffDTO);
        public void UpdateDailyReport(ReportDTO reportDTO);
        public void MarkDailyReportFinalised(ReportDTO reportDTO);
        
        public ReportDTO GetById(int reportId);
        public IEnumerable<ReportDTO> GetAllReports();
        
        public IEnumerable<ReportDTO> FindReportsByStaffId(StaffDTO staffDTO);
        
        public int CreateSprintReport();
        public void UpdateSprintReport();
        public void FixReportDTO(ReportDTO reportDTO);
    }
}