using Lab_6_BLL.DTO;

namespace Lab_6.UI.ViewModels
{
    public class TaskVM
    {
        public enum TaskState
        {
            Open,
            Active,
            Resolved
        }

        public string Name { get; set; }
        public string Description { get; set; }
        
        public int StaffId { get; set; }

        public TaskState State { get; set; }
        
        public string Comment { get; set; }

        public TaskVM(string name, string description, int staffId = -1, TaskState state = TaskState.Open, string comment = "")
        {
            Name = name;
            Description = description;
            State = state;
            StaffId = staffId;
            Comment = comment;
        }
        
        public TaskVM(TaskDTO taskDTO)
        {
            Name = taskDTO.Name;
            Description = taskDTO.Description;
            State = (TaskState) taskDTO.State;
            StaffId = taskDTO.StaffId;
            Comment = taskDTO.Comment;
        }

        public TaskDTO ToTaskDTO()
        {
            return new TaskDTO(-1, Name, Description, (TaskDTO.TaskState) State, StaffId, Comment);
        }
    }
}