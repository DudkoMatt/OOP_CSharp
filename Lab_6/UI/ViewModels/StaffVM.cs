using System.Collections.Generic;
using Lab_6_BLL.DTO;

namespace Lab_6.UI.ViewModels
{
    public class StaffVM
    {
        public string Name { get; set; }
        public int MentorId { get; set; }
        
        public List<int> SubordinatesId { get; set; }

        public StaffVM(string name, int mentorId = 0, List<int> subordinatesId = null)
        {
            Name = name;
            MentorId = mentorId;
            SubordinatesId = subordinatesId ?? new List<int>();
        }
        
        public StaffVM(StaffDTO staffDTO)
        {
            Name = staffDTO.Name;
            MentorId = staffDTO.MentorId;
            SubordinatesId = staffDTO.SubordinatesId;
        }

        public StaffDTO ToStaffDTO()
        {
            return new StaffDTO(-1, Name, MentorId, SubordinatesId);
        }
    }
}