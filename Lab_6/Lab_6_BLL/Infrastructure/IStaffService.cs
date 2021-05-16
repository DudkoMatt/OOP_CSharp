using System.Collections.Generic;
using Lab_6_BLL.DTO;

namespace Lab_6_BLL.Infrastructure
{
    public interface IStaffService
    {
        public int CreateStaff(StaffDTO staffDTO);

        public IEnumerable<StaffDTO> GetAllStaff();
        
        public void SetMentor(StaffDTO staffDTO, StaffDTO mentorStaffDTO);
        public int GetMentorId(StaffDTO staffDTO);

        public StaffDTO GetById(int staffId);

        public void AddSubordinate(StaffDTO staffDTO, StaffDTO subordinateStaffDTO);
        public void RemoveSubordinate(StaffDTO staffDTO, StaffDTO subordinateStaffDTO);
        public void FixStaffDTO(StaffDTO staffDTO);
    }
}