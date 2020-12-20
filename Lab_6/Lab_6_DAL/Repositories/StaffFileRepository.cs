using System;
using System.Collections.Generic;
using System.IO;
using Lab_6_DAL.Entities;
using Lab_6_DAL.Exceptions;
using Lab_6_DAL.Infrastructure;

namespace Lab_6_DAL.Repositories
{
    public class StaffFileRepository : FileRepository<StaffDAL>
    {
        public StaffFileRepository(string directoryPath) : base(directoryPath)
        { }

        public override StaffDAL Get(int id)
        {
            if (!File.Exists($"{DirectoryPath}/{id}.txt")) throw new StaffIdNotFoundException();

            string name;
            int mentorId;
            var subordinatesId = new List<int>();
            
            using (var sr = new StreamReader($"{DirectoryPath}/{id}.txt"))
            {
                name = sr.ReadLine();
                mentorId = Convert.ToInt32(sr.ReadLine());
                while (!sr.EndOfStream)
                {
                    subordinatesId.Add(Convert.ToInt32(sr.ReadLine()));
                }
            }

            return new StaffDAL(id, name, mentorId, subordinatesId);
        }

        public override void Create(StaffDAL item)
        {
            File.Create($"{DirectoryPath}/{item.Id}.txt").Close();

            using var sw = new StreamWriter($"{DirectoryPath}/{item.Id}.txt");
            sw.WriteLine(item.Name);
            sw.WriteLine(item.MentorId);
            foreach (var i in item.SubordinatesId)
            {
                sw.WriteLine(i);
            }
        }

        public override void Update(StaffDAL item)
        {
            Create(item);
        }
    }
}