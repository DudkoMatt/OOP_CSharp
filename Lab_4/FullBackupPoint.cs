using System;
using System.Collections.Generic;

namespace Lab_4
{
    public class FullBackupPoint : RestorePointType
    {
        public FullBackupPoint(RestorePointType previousPoint, Dictionary<string, FileRestoreCopyInfo> info, DateTime creationDate) : base(previousPoint, info, creationDate)
        {
            
        }
    }
}