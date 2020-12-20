using System.Collections.Generic;
using Lab_6_BLL.DTO;

namespace Lab_6.UI.ViewModels
{
    public class StaffVM
    {
        public int Id { get; }
        public string Name { get; }
        public int MentorId { get; }
        
        public readonly List<int> SubordinatesId;

        public StaffVM(int id, string name, int mentorId = 0, List<int> subordinatesId = null)
        {
            Id = id;
            Name = name;
            MentorId = mentorId;
            SubordinatesId = subordinatesId ?? new List<int>();
        }
        
        public StaffVM(StaffDTO staffDTO)
        {
            Id = staffDTO.Id;
            Name = staffDTO.Name;
            MentorId = staffDTO.MentorId;
            SubordinatesId = staffDTO.SubordinatesId;
        }

        public StaffDTO ToStaffDTO()
        {
            return new StaffDTO(Id, Name, MentorId, SubordinatesId);
        }
    }
}