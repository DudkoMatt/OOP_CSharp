using System;
using System.Collections.Generic;
using Lab_6_DAL.Infrastructure;

namespace Lab_6_DAL.Entities
{
    public class ReportDAL : IEntity
    {
        public int Id { get; }
        public readonly DateTime CreationDateTime;
        public int StaffId;
        public bool Finalised;
        public List<int> ResolvedTasks;
        public List<int> ChangesId;
        
        public ReportDAL(int id, DateTime dateTime, int staffId, bool finalised, List<int> resolvedTasks = null, List<int> changesId = null)
        {
            Id = id;
            CreationDateTime = dateTime;
            StaffId = staffId;
            Finalised = finalised;
            ResolvedTasks = resolvedTasks ?? new List<int>();
            ChangesId = changesId ?? new List<int>();
        }
    }
}