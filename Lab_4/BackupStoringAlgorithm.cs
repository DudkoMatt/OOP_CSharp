namespace Lab_4
{
    public abstract class BackupStoringAlgorithm
    {
        public readonly string BackupPath;

        public BackupStoringAlgorithm(string backupPath = ".")
        {
            BackupPath = backupPath;
        }

        public abstract void CopyFile(string filePath);
        public abstract void RestoreFile(string filePath);
    }
}