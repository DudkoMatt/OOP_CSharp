using System;
using System.Collections.Generic;
using Lab_6_DAL.Infrastructure;

namespace Lab_6_DAL.Entities
{
    public class ReportDAL : IEntity
    {
        public int Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public int StaffId { get; set; }
        public bool Finalised { get; set; }
        public List<int> ResolvedTasks { get; set; }
        public List<int> ChangesId { get; set; }
        
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