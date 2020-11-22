using System;
using System.Collections.Generic;

namespace Lab_4
{
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

        public RestorePointType Clean(RestorePointType lastPoint, out bool areMorePointsLeft)
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
            
            return selectedAlgorithm.Clean(lastPoint, out areMorePointsLeft);
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