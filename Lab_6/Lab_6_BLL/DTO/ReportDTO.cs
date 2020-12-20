using System;
using System.Collections.Generic;
using Lab_6_DAL.Entities;

namespace Lab_6_BLL.DTO
{
    public class ReportDTO
    {
        public readonly int Id;
        public readonly DateTime CreationDateTime;
        public int StaffId;
        public bool Finalised;
        public List<int> ResolvedTasks;

        // Id тасков, в которых в день создания совершены изменения
        public List<int> ChangesTasksId;

        public ReportDTO(int id, DateTime dateTime, int staffId, bool finalised = false, List<int> resolvedTasks = null, List<int> changesId = null)
        {
            Id = id;
            CreationDateTime = dateTime;
            StaffId = staffId;
            Finalised = finalised;
            ResolvedTasks = resolvedTasks ?? new List<int>();
            ChangesTasksId = changesId ?? new List<int>();
        }
        
        public ReportDTO(ReportDAL reportDAL)
        {
            Id = reportDAL.Id;
            CreationDateTime = reportDAL.CreationDateTime;
            StaffId = reportDAL.StaffId;
            Finalised = reportDAL.Finalised;
            ResolvedTasks = reportDAL.ResolvedTasks;
            ChangesTasksId = reportDAL.ChangesId;
        }

        public ReportDAL ToReportDAL()
        {
            return new ReportDAL(Id, CreationDateTime, StaffId,  Finalised, ResolvedTasks, ChangesTasksId);
        }

        public void AddResolveTask(int taskId)
        {
            if (!ResolvedTasks.Contains(taskId))
                ResolvedTasks.Add(taskId);
        }

        public void UpdateChanges(List<int> changesId)
        {
            ChangesTasksId = changesId;
        }
    }
}