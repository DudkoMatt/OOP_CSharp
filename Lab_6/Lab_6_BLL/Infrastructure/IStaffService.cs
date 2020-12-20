using System.Collections.Generic;
using Lab_6_BLL.DTO;

namespace Lab_6_BLL.Infrastructure
{
    public interface IStaffService
    {
        public int CreateStaff(string name);

        public IEnumerable<StaffDTO> GetAllStaff();
        
        public void SetMentorId(int staffId, int mentorId = 0);
        public int GetMentorId(int staffId);

        public void AddSubordinate(int staffId, int subordinateId);
        public void RemoveSubordinate(int staffId, int subordinateId);
    }
}