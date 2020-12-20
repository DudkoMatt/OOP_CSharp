using System;
using System.Collections.Generic;
using Lab_6_BLL.DTO;

namespace Lab_6.UI.ViewModels
{
    public class ReportVM
    {
        public readonly int Id;
        public readonly DateTime CreationDateTime;
        public int StaffId;
        public bool Finalised;
        public List<int> ResolvedTasks;

        // Id тасков, в которых в день создания совершены изменения
        public List<int> ChangesTasksId;
        
        public ReportVM(int id, DateTime dateTime, int staffId, bool finalised = false, List<int> resolvedTasks = null, List<int> changesId = null)
        {
            Id = id;
            CreationDateTime = dateTime;
            StaffId = staffId;
            Finalised = finalised;
            ResolvedTasks = resolvedTasks ?? new List<int>();
            ChangesTasksId = changesId ?? new List<int>();
        }
        
        public ReportVM(ReportDTO reportDTO)
        {
            Id = reportDTO.Id;
            CreationDateTime = reportDTO.CreationDateTime;
            StaffId = reportDTO.StaffId;
            Finalised = reportDTO.Finalised;
            ResolvedTasks = reportDTO.ResolvedTasks;
            ChangesTasksId = reportDTO.ChangesTasksId;
        }

        public ReportDTO ToReportDto()
        {
            return new ReportDTO(Id, CreationDateTime, StaffId, Finalised, ResolvedTasks, ChangesTasksId);
        }
    }
}