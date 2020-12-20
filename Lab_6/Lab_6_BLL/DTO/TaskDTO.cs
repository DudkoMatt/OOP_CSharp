using Lab_6_DAL.Entities;

namespace Lab_6_BLL.DTO
{
    public class TaskDTO
    {
        public enum TaskState
        {
            Open,
            Active,
            Resolved
        }
        
        public int Id { get; }

        public string Name { get; }
        public string Description { get; }
        
        public int StaffId { get; set; }

        public TaskState State;
        
        public string Comment { get; set; }

        public TaskDTO(int id, string name, string description, TaskState state, int staffId)
        {
            Id = id;
            Name = name;
            Description = description;
            State = state;
            StaffId = staffId;
        }

        public TaskDTO(TaskDAL taskDAL)
        {
            Id = taskDAL.Id;
            Name = taskDAL.Name;
            Description = taskDAL.Description;
            State = (TaskState) taskDAL.State;
            StaffId = taskDAL.StaffId;
            Comment = taskDAL.Comment;
        }

        public TaskDAL ToTaskDAL()
        {
            return new TaskDAL(Id, Name, Description, (int) State, StaffId, Comment);
        }
    }
}