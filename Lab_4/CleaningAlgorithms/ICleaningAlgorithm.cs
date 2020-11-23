namespace Lab_4
{
    public interface ICleaningAlgorithm
    {
        // ToDO: список точек, которые нужно удалить + удаление из диска
        public RestorePoint Clean(RestorePoint lastPoint, out bool areMorePointsLeft);  // Очистка, возвращается последняя точка
        public long CountLeftPoints(RestorePoint lastPoint);  // Подсчет без удаления
    }
}