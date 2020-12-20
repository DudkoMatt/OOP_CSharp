using System.Collections.Generic;
using Lab_6_DAL.Entities;

namespace Lab_6_BLL.DTO
{
    public class StaffDTO
    {
        public int Id { get; }
        public string Name { get; }
        public int MentorId { get; set; }
        
        public readonly List<int> SubordinatesId;

        public StaffDTO(int id, string name, int mentorId = 0, List<int> subordinatesId = null)
        {
            Id = id;
            Name = name;
            MentorId = mentorId;
            SubordinatesId = subordinatesId ?? new List<int>();
        }

        public StaffDTO(StaffDAL staffDAL)
        {
            Id = staffDAL.Id;
            Name = staffDAL.Name;
            SubordinatesId = new List<int>(staffDAL.SubordinatesId);
            MentorId = staffDAL.MentorId;
        }

        public void AddSubordinate(int subordinateId)
        {
            if (!SubordinatesId.Contains(subordinateId))
                SubordinatesId.Add(subordinateId);
        }

        public void RemoveSubordinate(int subordinateId)
        {
            SubordinatesId.Remove(subordinateId);
        }

        public StaffDAL ToStaffDAL()
        {
            return new StaffDAL(Id, Name, MentorId, SubordinatesId);
        }
    }
}