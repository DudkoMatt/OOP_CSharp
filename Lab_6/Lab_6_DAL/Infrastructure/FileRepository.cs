using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab_6_DAL.Infrastructure
{
    public abstract class FileRepository<T> : IRepository<T> where T : IEntity
    {
        protected string DirectoryPath { get; }

        protected FileRepository(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            DirectoryPath = directoryPath;
        }

        public abstract T Get(int id);

        public bool TryGet(int id)
        {
            return File.Exists($"{DirectoryPath}/{id}.txt");
        }

        public IEnumerable<T> GetAll()
        {
            var list = new List<T>();
            foreach (var fileName in Directory.GetFiles(DirectoryPath))
            {
                if (!Regex.IsMatch(fileName, "[0-9]*.txt")) continue;
                var id = Convert.ToInt32(fileName.Substring(DirectoryPath.Length, fileName.Length - 4 - DirectoryPath.Length));
                list.Add(Get(id));
            }

            return list;
        }

        public int GetMaxId()
        {
            var maxId = -1;
            foreach (var fileName in Directory.GetFiles(DirectoryPath))
            {
                if (!Regex.IsMatch(fileName, "[0-9]*.txt")) continue;
                var tmp = Convert.ToInt32(fileName.Substring(DirectoryPath.Length, fileName.Length - 4 - DirectoryPath.Length));
                maxId = maxId < tmp ? tmp : maxId;
            }

            return maxId;
        }

        public abstract void Create(T item);
        public abstract void Update(T item);

        public void Delete(int id)
        {
            File.Delete($"{DirectoryPath}/{id}.txt");
        }
    }
}