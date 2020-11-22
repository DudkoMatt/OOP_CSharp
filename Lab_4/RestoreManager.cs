using System;
using System.Collections.Generic;
using System.IO;

namespace Lab_4
{
    public class RestoreManager
    {
        public List<string> ObjectsForBackup { get; }
        public RestorePointType LastPoint { get; protected set; }
        public BackupStoringAlgorithm StoringAlgorithm { get; set; }
        public ICleaningAlgorithm CleaningAlgorithm { get; set; }
        
        public RestoreManager()
        {
            ObjectsForBackup = new List<string>();
            LastPoint = null;
            StoringAlgorithm = new SeparateStoringAlgorithm();
            CleaningAlgorithm = null;
        }
        
        public void CreateCustomBackup(IBackupCreator backupCreator)
        {
            LastPoint = backupCreator.CreateBackup(LastPoint, ObjectsForBackup, StoringAlgorithm);
        }

        public void CreateFullBackup()
        {
            LastPoint = new FullBackupCreator().CreateBackup(LastPoint, ObjectsForBackup, StoringAlgorithm);
        }

        public void CreateIncrementalBackup()
        {
            if (LastPoint == null) throw new IncrementalBackupCannotBeFirstException();
            var newPoint = new IncrementalBackupCreator().CreateBackup(LastPoint, ObjectsForBackup, StoringAlgorithm);
            if (newPoint == null) throw new NoChangesForIncrementalBackupException();
            LastPoint = newPoint;
        }

        public void Clean()
        {
            LastPoint = CleaningAlgorithm.Clean(LastPoint, out var areMorePointsLeft);
            if (areMorePointsLeft)
                throw new MorePointsLeftException();
        }
        
        public long CountLeftPoints()
        {
            return CleaningAlgorithm.CountLeftPoints(LastPoint);
        }

        public void AddObjectForBackupList(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException();
            if (!ObjectsForBackup.Contains(path))
                ObjectsForBackup.Add(path);
        }

        public void RemoveObjectFromBackupList(string path)
        {
            if (ObjectsForBackup.Contains(path)) ObjectsForBackup.Remove(path);
        }

        public void PrintHistory()
        {
            var point = LastPoint;
            while (point != null)
            {
                Console.WriteLine($"Point ID: {point.Id}");
                Console.WriteLine($"Point type: {point.GetType()}");
                Console.WriteLine($"Total size: {point.Size()} bytes");
                Console.WriteLine($"Creation date: {point.CreationDate}");
                Console.WriteLine("Saved files:\n");

                foreach (var (filepath, fileInfo) in point.Info)
                {
                    Console.Write($"{filepath} ");
                    Console.WriteLine($"{fileInfo.BackupSize} bytes");
                }

                Console.WriteLine("-----------------\n");
                
                point = point.PreviousPoint;
            }
        }
    }
}