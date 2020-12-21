using System;
using System.IO;
using System.Linq;
using Lab_6_DAL.Entities;
using Lab_6_DAL.Exceptions;
using Lab_6_DAL.Infrastructure;

namespace Lab_6_DAL.Repositories
{
    public class TaskFileRepository : FileRepository<TaskDAL>
    {
        private int _nextTaskId;

        public TaskFileRepository(string directoryPath) : base(directoryPath)
        {
            _nextTaskId = GetMaxId() + 1;
        }

        public override TaskDAL Get(int id)
        {
            if (!File.Exists($"{DirectoryPath}/{id}.txt")) throw new TaskIdNotFoundException();

            string name;
            string description;
            int staffId;
            int state;
            string comment;
            
            using (var sr = new StreamReader($"{DirectoryPath}/{id}.txt"))
            {
                name = sr.ReadLine();
                description = sr.ReadLine();
                staffId = Convert.ToInt32(sr.ReadLine());
                state = Convert.ToInt32(sr.ReadLine());
                comment = sr.ReadLine();
            }

            return new TaskDAL(id, name, description, state, staffId, comment);
        }

        protected override void WriteToFile(TaskDAL item)
        {
            using var sw = File.CreateText($"{DirectoryPath}/{item.Id}.txt");
            sw.WriteLine(item.Name);
            sw.WriteLine(item.Description);
            sw.WriteLine(item.StaffId);
            sw.WriteLine(item.State);
            sw.WriteLine(item.Comment);
        }

        public override int Create(TaskDAL item)
        {
            item.Id = _nextTaskId;
            WriteToFile(item);
            return _nextTaskId++;
        }

        public override void Update(TaskDAL item)
        {
            WriteToFile(item);
        }

        public override void Fix(TaskDAL item)
        {
            var task = GetAll().First(t =>
                t.Comment == item.Comment &&
                t.Description == item.Description &&
                t.Name == item.Name &&
                t.State == item.State &&
                t.StaffId == item.StaffId
            );
            item.Id = task.Id;
        }
    }
}