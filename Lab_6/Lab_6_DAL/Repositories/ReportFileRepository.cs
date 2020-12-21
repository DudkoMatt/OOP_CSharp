using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab_6_DAL.Entities;
using Lab_6_DAL.Exceptions;
using Lab_6_DAL.Infrastructure;

namespace Lab_6_DAL.Repositories
{
    public class ReportFileRepository : FileRepository<ReportDAL>
    {
        private int _nextReportId;

        public ReportFileRepository(string directoryPath) : base(directoryPath)
        {
            _nextReportId = GetMaxId() + 1;
        }
        
        public override ReportDAL Get(int id)
        {
            if (!File.Exists($"{DirectoryPath}/{id}.txt")) throw new ReportIdNotFoundException();

            var resolvedTasks = new List<int>();
            var changesId = new List<int>();

            using var sr = new StreamReader($"{DirectoryPath}/{id}.txt");
            var creationDateTime = new DateTime(Convert.ToInt64(sr.ReadLine()));
            var staffId = Convert.ToInt32(sr.ReadLine());
            var finalised = Convert.ToBoolean(sr.ReadLine());
            var k = Convert.ToInt32(sr.ReadLine());
            for (var i = 0; i < k; i++)
            {
                resolvedTasks.Add(Convert.ToInt32(sr.ReadLine()));
            }
                
            k = Convert.ToInt32(sr.ReadLine());
            for (var i = 0; i < k; i++)
            {
                changesId.Add(Convert.ToInt32(sr.ReadLine()));
            }

            return new ReportDAL(id, creationDateTime, staffId, finalised, resolvedTasks, changesId);
        }

        protected override void WriteToFile(ReportDAL item)
        {
            File.Create($"{DirectoryPath}/{item.Id}.txt").Close();
            using var sw = new StreamWriter($"{DirectoryPath}/{item.Id}.txt");
            sw.WriteLine(item.CreationDateTime.Ticks);
            sw.WriteLine(item.StaffId);
            sw.WriteLine(item.Finalised);
            sw.WriteLine(item.ResolvedTasks.Count);
            foreach (var resolvedTaskId in item.ResolvedTasks)
            {
                sw.WriteLine(resolvedTaskId);
            }
            sw.WriteLine(item.ChangesId.Count);
            foreach (var changesId in item.ChangesId)
            {
                sw.WriteLine(changesId);
            }
        }

        public override int Create(ReportDAL item)
        {
            item.Id = _nextReportId;
            WriteToFile(item);
            return _nextReportId++;
        }

        public override void Update(ReportDAL item)
        {
            WriteToFile(item);
        }

        public override void Fix(ReportDAL item)
        {
            var report = GetAll().First(r =>
                r.ChangesId.OrderBy(i => i).SequenceEqual(item.ChangesId.OrderBy(i => i)) &&
                r.ResolvedTasks.OrderBy(i => i).SequenceEqual(item.ResolvedTasks.OrderBy(i => i)) &&
                r.StaffId == item.StaffId &&
                r.CreationDateTime == item.CreationDateTime &&
                r.Finalised == item.Finalised
            );
            item.Id = report.Id;
        }
    }
}