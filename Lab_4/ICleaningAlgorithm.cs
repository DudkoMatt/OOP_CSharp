using System;
using System.Collections.Generic;

namespace Lab_4
{
    public interface ICleaningAlgorithm
    {
        public RestorePointType Clean(RestorePointType lastPoint);  // Очистка, возвращается последняя точка
        public long CountLeftPoints(RestorePointType lastPoint);  // Подсчет без удаления
    }

    public class CountCleaningAlgorithm : ICleaningAlgorithm
    {
        public readonly long LastNPoints;

        public CountCleaningAlgorithm(long lastNPoints)
        {
            LastNPoints = lastNPoints;
        }

        public RestorePointType Clean(RestorePointType lastPoint)
        {
            var k = LastNPoints - 1;
            var point = lastPoint;

            // ToDO: prevPoint и point
            while (point != null && (k > 0 || point is IncrementalBackupPoint))
            {
                point = point.PreviousPoint;
                --k;
            }

            if (k <= 0 && point != null) point.PreviousPoint = null;
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

    // ToDO: покрыть тестами
    public class DateCleaningAlgorithm : ICleaningAlgorithm
    {
        public readonly DateTime Date;

        public DateCleaningAlgorithm(DateTime date)
        {
            Date = date;
        }
        
        public RestorePointType Clean(RestorePointType lastPoint)
        {
            var point = lastPoint;
            RestorePointType prevPoint = null;
            
            while (point != null && (point.CreationDate > Date || point is IncrementalBackupPoint))
            {
                prevPoint = point;
                point = point.PreviousPoint;
            }

            if (prevPoint != null)
                prevPoint.PreviousPoint = null;

            return lastPoint;
        }

        public long CountLeftPoints(RestorePointType lastPoint)
        {
            long k = 0;
            var point = lastPoint;

            while (point != null && (point.CreationDate > Date || point is IncrementalBackupPoint))
            {
                point = point.PreviousPoint;
                k++;
            }

            return k;
        }
    }

    public class SizeCleaningAlgorithm : ICleaningAlgorithm
    {
        public readonly long MaxSize;

        public SizeCleaningAlgorithm(long maxSizeInBytes)
        {
            MaxSize = maxSizeInBytes;
        }

        public RestorePointType Clean(RestorePointType lastPoint)
        {
            var point = lastPoint;
            RestorePointType prevPoint = null;
            long currentSize = 0;

            while (point != null && (currentSize < MaxSize || point is IncrementalBackupPoint))
            {
                if (point.Size() + currentSize <= MaxSize)
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

    public class HybridCleaningAlgorithm : ICleaningAlgorithm
    {
        // Коллекция алгоритмов

        public List<ICleaningAlgorithm> Algorithms { get; }
        public readonly bool MinimumFlag;

        public HybridCleaningAlgorithm(List<ICleaningAlgorithm> algorithms, bool minimumFlag)
        {
            Algorithms = algorithms;
            MinimumFlag = minimumFlag;
        }

        public RestorePointType Clean(RestorePointType lastPoint)
        {
            ICleaningAlgorithm selectedAlgorithm = null;
            long numberOfLeftPoints = 0;
            var isNumberSet = false;
            
            foreach (var algorithm in Algorithms)
            {
                if (MinimumFlag)
                {
                    // minimum
                    if (algorithm.CountLeftPoints(lastPoint) < numberOfLeftPoints || !isNumberSet)
                    {
                        isNumberSet = true;
                        numberOfLeftPoints = algorithm.CountLeftPoints(lastPoint);
                        selectedAlgorithm = algorithm;
                    }
                }
                else
                {
                    // maximum
                    if (algorithm.CountLeftPoints(lastPoint) > numberOfLeftPoints || !isNumberSet)
                    {
                        isNumberSet = true;
                        numberOfLeftPoints = algorithm.CountLeftPoints(lastPoint);
                        selectedAlgorithm = algorithm;
                    }
                }
            }
            
            if (selectedAlgorithm == null)
                throw new NullReferenceException("Nothing selected for cleaning algorithm");
            
            return selectedAlgorithm.Clean(lastPoint);
        }

        public long CountLeftPoints(RestorePointType lastPoint)
        {
            long numberOfLeftPoints = 0;
            var isNumberSet = false;
            
            foreach (var algorithm in Algorithms)
            {
                if (MinimumFlag)
                {
                    // minimum
                    if (algorithm.CountLeftPoints(lastPoint) < numberOfLeftPoints || !isNumberSet)
                    {
                        isNumberSet = true;
                        numberOfLeftPoints = algorithm.CountLeftPoints(lastPoint);
                    }
                }
                else
                {
                    // maximum
                    if (algorithm.CountLeftPoints(lastPoint) > numberOfLeftPoints || !isNumberSet)
                    {
                        isNumberSet = true;
                        numberOfLeftPoints = algorithm.CountLeftPoints(lastPoint);
                    }
                }
            }

            return numberOfLeftPoints;
        }
    }
}