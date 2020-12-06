using System.Collections.Generic;

namespace Lab_4
{
    public interface IBackupCreator
    {
        public RestorePoint CreateBackup(RestorePoint lastPoint, List<string> objectsForBackup, BackupStoringAlgorithm storingAlgorithm);
    }
}