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
        
        /*private FileRestoreCopyInfo CreateRestore(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            var fileRestoreCopyInfo = new FileRestoreCopyInfo(filePath, fileInfo.Length, DateTime.Now);
            // File.Copy(filePath, _pathWhereWeNeedToStoreOurBackup);
            
            return fileRestoreCopyInfo;
        }*/
    }

    // =============================================================================================================    

    public interface IBackupCreator
    {
        public RestorePointType CreateBackup(RestorePointType lastPoint, List<string> objectsForBackup, BackupStoringAlgorithm storingAlgorithm);
    }
    
    public class FullBackupCreator : IBackupCreator
    {
        public RestorePointType CreateBackup(RestorePointType lastPoint, List<string> objectsForBackup, BackupStoringAlgorithm storingAlgorithm)
        {
            var list = new Dictionary<string, FileRestoreCopyInfo>();
            foreach (var filePath in objectsForBackup)
            {
                list.Add(filePath, new FileRestoreCopyInfo(filePath, new FileInfo(filePath).Length, DateTime.Now, storingAlgorithm.BackupPath));
                // storingAlgorithm.CopyFile(filePath);
            }
            
            return new FullBackupPoint(lastPoint, list, DateTime.Now);
        }
    }

    // Проблема, что это не совсем самостоятельный класс
    // Конкретно: нельзя создать incremental backup без предварительно созданного full backup
    public class IncrementalBackupCreator : IBackupCreator
    {
        public RestorePointType CreateBackup(RestorePointType lastPoint, List<string> objectsForBackup, BackupStoringAlgorithm storingAlgorithm)
        {
            var lastFullBackupPoint = lastPoint;
            while (lastFullBackupPoint != null && !(lastFullBackupPoint is FullBackupPoint))
                lastFullBackupPoint = lastFullBackupPoint.PreviousPoint;

            if (lastFullBackupPoint == null)
                throw new IncrementalBackupCannotBeFirstException();
            
            var list = new Dictionary<string, FileRestoreCopyInfo>();
            foreach (var filePath in objectsForBackup)
            {
                // Упрощение: Если нет, или изменился размер файла
                if (!lastPoint.Info.ContainsKey(filePath) ||
                    lastPoint.Info[filePath].BackupSize != new FileInfo(filePath).Length)
                {
                    var point = lastPoint;
                    while (!point.Info.ContainsKey(filePath) && !(point is FullBackupPoint))
                        point = point.PreviousPoint;

                    var size = new FileInfo(filePath).Length;
                    if (!point.Info.ContainsKey(filePath))
                        size = Math.Abs(size - point.Info[filePath].BackupSize);
                    
                    list.Add(filePath,new FileRestoreCopyInfo(filePath, size, DateTime.Now, storingAlgorithm.BackupPath));
                    // storingAlgorithm.CopyFile(filePath);
                }
            }

            if (list.Count == 0) return null;
            
            return new IncrementalBackupPoint(lastPoint, list, DateTime.Now);
        }
    }
    
    public class NoChangesForIncrementalBackupException : Exception
    {
        public NoChangesForIncrementalBackupException()
        {
        }

        public NoChangesForIncrementalBackupException(string message)
            : base(message)
        {
        }

        public NoChangesForIncrementalBackupException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    
    public class MorePointsLeftException : Exception
    {
        public MorePointsLeftException()
        {
        }

        public MorePointsLeftException(string message)
            : base(message)
        {
        }

        public MorePointsLeftException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}