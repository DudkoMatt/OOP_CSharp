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
    
    public class SeparateStoringAlgorithm : BackupStoringAlgorithm
    {
        public SeparateStoringAlgorithm(string backupPath = ".") : base(backupPath)
        {
        }

        public override void CopyFile(string filePath)
        {
            throw new System.NotImplementedException();
        }

        public override void RestoreFile(string filePath)
        {
            throw new System.NotImplementedException();
        }
    }
    
    public class CommonStoringAlgorithm : BackupStoringAlgorithm
    {
        public CommonStoringAlgorithm(string backupPath = ".") : base(backupPath)
        {
        }

        public override void CopyFile(string filePath)
        {
            throw new System.NotImplementedException();
        }

        public override void RestoreFile(string filePath)
        {
            throw new System.NotImplementedException();
        }
    }
}