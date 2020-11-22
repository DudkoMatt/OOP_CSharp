namespace Lab_4
{
    public interface ICleaningAlgorithm
    {
        public RestorePointType Clean(RestorePointType lastPoint, out bool areMorePointsLeft);  // Очистка, возвращается последняя точка
        public long CountLeftPoints(RestorePointType lastPoint);  // Подсчет без удаления
    }
}