using System;
using System.Collections.Generic;

namespace Lab_4
{
    public class FullBackupPoint : RestorePoint
    {
        public FullBackupPoint(RestorePoint previousPoint, Dictionary<string, FileRestoreCopyInfo> info, DateTime creationDate) : base(previousPoint, info, creationDate)
        {
            
        }
    }
}