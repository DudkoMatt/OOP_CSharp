using System.Collections.Generic;
using System.IO;

namespace Lab_4
{
    public class FullBackupCreator : IBackupCreator
    {
        public RestorePointType CreateBackup(RestorePointType lastPoint, List<string> objectsForBackup, BackupStoringAlgorithm storingAlgorithm)
        {
            var list = new Dictionary<string, FileRestoreCopyInfo>();
            foreach (var filePath in objectsForBackup)
            {
                list.Add(filePath, new FileRestoreCopyInfo(filePath, new FileInfo(filePath).Length, DateTimeProvider.Now, storingAlgorithm.BackupPath));
                // storingAlgorithm.CopyFile(filePath);
            }
            
            return new FullBackupPoint(lastPoint, list, DateTimeProvider.Now);
        }
    }
}