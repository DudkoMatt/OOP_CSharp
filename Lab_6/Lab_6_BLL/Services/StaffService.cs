using System.Collections.Generic;
using System.Linq;
using Lab_6_BLL.DTO;
using Lab_6_DAL.Entities;
using Lab_6_DAL.Infrastructure;
using Lab_6_BLL.Infrastructure;

namespace Lab_6_BLL.Services
{
    public class StaffService : IStaffService
    {
        private int _nextStaffId;

        private IRepository<StaffDAL> _repository;
        
        public StaffService(IRepository<StaffDAL> repository)
        {
            _repository = repository;
            _nextStaffId = repository.GetMaxId() + 1;
        }
        
        public int CreateStaff(string name)
        {
            var staff = new StaffDTO(_nextStaffId, name);
            if (_nextStaffId != 0)
                AddSubordinate(0, _nextStaffId);
            
            _repository.Create(staff.ToStaffDAL());
            return _nextStaffId++;
        }

        public int GetMentorId(int staffId)
        {
            return new StaffDTO(_repository.Get(staffId)).MentorId;
        }

        public void SetMentorId(int staffId, int mentorId = 0)
        {
            var temp = new StaffDTO(_repository.Get(staffId));
            RemoveSubordinate(temp.MentorId, staffId);
            temp.MentorId = mentorId;
            _repository.Update(temp.ToStaffDAL());
            AddSubordinate(mentorId, staffId);
        }

        public void AddSubordinate(int mentorId, int subordinateId)
        {
            var temp = new StaffDTO(_repository.Get(mentorId));
            temp.AddSubordinate(subordinateId);
            _repository.Update(temp.ToStaffDAL());
        }

        public void RemoveSubordinate(int mentorId, int subordinateId)
        {
            var temp = new StaffDTO(_repository.Get(mentorId));
            temp.RemoveSubordinate(subordinateId);
            _repository.Update(temp.ToStaffDAL());
        }

        public IEnumerable<StaffDTO> GetAllStaff()
        {
            return _repository.GetAll().Select(t=> new StaffDTO(t));
        }
    }
}