using System;
using System.Collections.Generic;

namespace Lab_4
{
    public class IncrementalBackupPoint : RestorePoint
    {
        public IncrementalBackupPoint(RestorePoint previousPoint, Dictionary<string, FileRestoreCopyInfo> info, DateTime creationDate) : base(previousPoint, info, creationDate)
        {
            
        }
    }
}