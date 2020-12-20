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
        
        public int Id { get; }

        public string Name { get; }
        public string Description { get; }
        
        public int StaffId { get; }

        public TaskState State;
        
        public string Comment { get; set; }

        public TaskVM(int id, string name, string description, TaskState state, int staffId)
        {
            Id = id;
            Name = name;
            Description = description;
            State = state;
            StaffId = staffId;
        }
        
        public TaskVM(TaskDTO taskDTO)
        {
            Id = taskDTO.Id;
            Name = taskDTO.Name;
            Description = taskDTO.Description;
            State = (TaskState) taskDTO.State;
            StaffId = taskDTO.StaffId;
        }

        public TaskDTO ToTaskDTO()
        {
            return new TaskDTO(Id, Name, Description, (TaskDTO.TaskState) State, StaffId);
        }
    }
}