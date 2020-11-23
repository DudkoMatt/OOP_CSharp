using System;

namespace Lab_4
{
    public class DateCleaningAlgorithm : ICleaningAlgorithm
    {
        public readonly DateTime Date;

        public DateCleaningAlgorithm(DateTime date)
        {
            Date = date;
        }
        
        public RestorePoint Clean(RestorePoint lastPoint, out bool areMorePointsLeft)
        {
            areMorePointsLeft = false;
            var point = lastPoint;
            RestorePoint prevPoint = null;
            
            while (point != null && (point.CreationDate >= Date || point is IncrementalBackupPoint))
            {
                prevPoint = point;
                point = point.PreviousPoint;
            }

            if (prevPoint != null)
                prevPoint.PreviousPoint = null;

            return lastPoint;
        }

        public long CountLeftPoints(RestorePoint lastPoint)
        {
            long k = 0;
            var point = lastPoint;

            while (point != null && (point.CreationDate >= Date || point is IncrementalBackupPoint))
            {
                point = point.PreviousPoint;
                k++;
            }

            return k;
        }
    }
}