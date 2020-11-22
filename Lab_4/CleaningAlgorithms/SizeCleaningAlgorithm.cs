namespace Lab_4
{
    public class SizeCleaningAlgorithm : ICleaningAlgorithm
    {
        public readonly long MaxSize;

        public SizeCleaningAlgorithm(long maxSizeInBytes)
        {
            MaxSize = maxSizeInBytes;
        }

        public RestorePointType Clean(RestorePointType lastPoint, out bool areMorePointsLeft)
        {
            areMorePointsLeft = false;
            var point = lastPoint;
            RestorePointType prevPoint = null;
            long currentSize = 0;

            while (point != null && (currentSize < MaxSize || point is IncrementalBackupPoint))
            {
                if (point.Size() + currentSize <= MaxSize || point is IncrementalBackupPoint)
                {
                    currentSize += point.Size();
                    prevPoint = point;
                    point = point.PreviousPoint;
                    continue;
                }
                break;
            }

            if (prevPoint != null)
                prevPoint.PreviousPoint = null;

            if (currentSize > MaxSize) areMorePointsLeft = true;
            
            return lastPoint;
        }

        public long CountLeftPoints(RestorePointType lastPoint)
        {
            long k = 0;
            var point = lastPoint;
            long currentSize = 0;

            while (point != null && (currentSize < MaxSize || point is IncrementalBackupPoint))
            {
                if (point.Size() + currentSize <= MaxSize)
                {
                    k++;
                    currentSize += point.Size();
                    point = point.PreviousPoint;
                    continue;
                }
                break;
            }

            return k;
        }
    }
}