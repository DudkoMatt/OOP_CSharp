using System;
using System.Collections.Generic;

namespace Lab_4
{
    public abstract class RestorePoint
    {
        private static ulong _nextId = 1;
        
        public RestorePoint PreviousPoint;
        public readonly ulong Id;
        public readonly Dictionary<string, FileRestoreCopyInfo> Info;
        public readonly DateTime CreationDate;
        
        public RestorePoint(RestorePoint previousPoint, Dictionary<string, FileRestoreCopyInfo> info, DateTime creationDate)
        {
            PreviousPoint = previousPoint;
            Id = _nextId++;
            Info = info;
            CreationDate = creationDate;
        }

        public long Size()
        {
            long allSize = 0;
            foreach (var (_, info) in Info)
            {
                allSize += info.BackupSize;
            }

            return allSize;
        }
    }
}