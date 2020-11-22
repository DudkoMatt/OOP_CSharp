using System;
using System.Collections.Generic;

namespace Lab_4
{
    public class IncrementalBackupPoint : RestorePointType
    {
        public IncrementalBackupPoint(RestorePointType previousPoint, Dictionary<string, FileRestoreCopyInfo> info, DateTime creationDate) : base(previousPoint, info, creationDate)
        {
            
        }
    }
}