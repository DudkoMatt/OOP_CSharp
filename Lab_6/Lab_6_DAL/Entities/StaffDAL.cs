using System.Collections.Generic;
using Lab_6_DAL.Infrastructure;

namespace Lab_6_DAL.Entities
{
    public class StaffDAL : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MentorId { get; set; }
        
        public List<int> SubordinatesId { get; set; }

        public StaffDAL(int id, string name, int mentorId = 0, List<int> subordinatesId = null)
        {
            Id = id;
            Name = name;
            MentorId = mentorId;
            SubordinatesId = subordinatesId ?? new List<int>();
        }
    }
}