using System;
using System.Collections.Generic;

namespace Lab_4
{
    public abstract class RestorePointType
    {
        private static ulong _nextId = 1;
        
        public RestorePointType PreviousPoint;
        public readonly ulong Id;
        public readonly Dictionary<string, FileRestoreCopyInfo> Info;
        public readonly DateTime CreationDate;
        
        public RestorePointType(RestorePointType previousPoint, Dictionary<string, FileRestoreCopyInfo> info, DateTime creationDate)
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
    
    public class FullBackupPoint : RestorePointType
    {
        public FullBackupPoint(RestorePointType previousPoint, Dictionary<string, FileRestoreCopyInfo> info, DateTime creationDate) : base(previousPoint, info, creationDate)
        {
            
        }
    }

    public class IncrementalBackupPoint : RestorePointType
    {
        public IncrementalBackupPoint(RestorePointType previousPoint, Dictionary<string, FileRestoreCopyInfo> info, DateTime creationDate) : base(previousPoint, info, creationDate)
        {
            
        }
    }
}