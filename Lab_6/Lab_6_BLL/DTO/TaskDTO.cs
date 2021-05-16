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
        
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        
        public int StaffId { get; set; }

        public TaskState State { get; set; }
        
        public string Comment { get; set; }

        public TaskDTO(int id, string name, string description, TaskState state, int staffId, string comment)
        {
            Id = id;
            Name = name;
            Description = description;
            State = state;
            StaffId = staffId;
            Comment = comment;
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