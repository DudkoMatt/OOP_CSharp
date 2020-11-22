using System.Collections.Generic;

namespace Lab_4
{
    public interface IBackupCreator
    {
        public RestorePointType CreateBackup(RestorePointType lastPoint, List<string> objectsForBackup, BackupStoringAlgorithm storingAlgorithm);
    }
}