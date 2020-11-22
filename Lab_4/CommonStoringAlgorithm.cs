namespace Lab_4
{
    // Данный алгоритм сохраняет файлы в архив
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