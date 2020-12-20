using Lab_6_DAL.Infrastructure;

namespace Lab_6_DAL.Entities
{
    public class TaskDAL : IEntity
    {
        public int Id { get; }

        public string Name { get; }
        public string Description { get; }
        
        public int StaffId { get; set; }

        public int State;
        
        public string Comment { get; set; }
        
        public TaskDAL(int id, string name, string description, int state, int staffId, string comment)
        {
            Id = id;
            Name = name;
            Description = description;
            State = state;
            StaffId = staffId;
            Comment = comment;
        }
    }
}