namespace Lab_4
{
    // Данный алгоритм сохраняет файлы в отдельную папку
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
}