namespace Lab_4
{
    public class CountCleaningAlgorithm : ICleaningAlgorithm
    {
        public readonly long LastNPoints;

        public CountCleaningAlgorithm(long lastNPoints)
        {
            LastNPoints = lastNPoints;
        }

        public RestorePointType Clean(RestorePointType lastPoint, out bool areMorePointsLeft)
        {
            areMorePointsLeft = false;
            var k = LastNPoints - 1;
            var point = lastPoint;
            
            while (point != null && (k > 0 || point is IncrementalBackupPoint))
            {
                point = point.PreviousPoint;
                --k;
            }

            if (k <= 0 && point != null) point.PreviousPoint = null;
            if (k < 0) areMorePointsLeft = true;
            return lastPoint;
        }

        public long CountLeftPoints(RestorePointType lastPoint)
        {
            var k = LastNPoints - 1;
            var point = lastPoint;

            while (point != null && (k > 0 || point is IncrementalBackupPoint))
            {
                point = point.PreviousPoint;
                --k;
            }

            return LastNPoints - k;
        }
    }
}