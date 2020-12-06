using System;
using System.Collections.Generic;
using System.IO;

namespace Lab_4
{
    public class IncrementalBackupCreator : IBackupCreator
    {
        public RestorePoint CreateBackup(RestorePoint lastPoint, List<string> objectsForBackup, BackupStoringAlgorithm storingAlgorithm)
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
                    
                    list.Add(filePath,new FileRestoreCopyInfo(filePath, size, DateTimeProvider.Now, storingAlgorithm.BackupPath));
                    // storingAlgorithm.CopyFile(filePath);
                }
            }

            if (list.Count == 0) throw new NoChangesForIncrementalBackupException();
            
            return new IncrementalBackupPoint(lastPoint, list, DateTimeProvider.Now);
        }
    }
}