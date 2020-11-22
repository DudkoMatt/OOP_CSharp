using System;

namespace Lab_4
{
    public class FileRestoreCopyInfo
    {
        public readonly DateTime CreationTime;
        public readonly long BackupSize;
        public readonly string FilePath;
        public readonly string BackupFilePath;

        public FileRestoreCopyInfo(string filePath, long backupSize, DateTime creationTime, string backupFilePath)
        {
            FilePath = filePath;
            BackupSize = backupSize;
            CreationTime = creationTime;
            BackupFilePath = backupFilePath;
        }
    }
}