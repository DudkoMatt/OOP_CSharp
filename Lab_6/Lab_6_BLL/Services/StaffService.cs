using System.Collections.Generic;
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
        
        /// <summary>
        /// Корень дерева сотрудников (тимлид)
        /// У Root'а всегда Id = 0
        /// </summary>
        private readonly Dictionary<int, StaffDTO> _allStaff;
        
        public StaffService(IRepository<StaffDAL> repository)
        {
            _repository = repository;
            _nextStaffId = repository.GetMaxId() + 1;

            _allStaff = new Dictionary<int, StaffDTO>();
            for (var i = 0; i < repository.GetMaxId(); i++)
            {
                if (_repository.TryGet(i))
                    _allStaff.Add(i, new StaffDTO(_repository.Get(i)));
            }
        }
        
        public int CreateStaff(string name)
        {
            var staff = new StaffDTO(_nextStaffId, name);
            _allStaff.Add(_nextStaffId, staff);
            if (_nextStaffId != 0) AddSubordinate(0, _nextStaffId);
            
            _repository.Create(staff.ToStaffDAL());
            if (_nextStaffId != 0) _repository.Update(_allStaff[0].ToStaffDAL());
            
            return _nextStaffId++;
        }

        public int GetMentorId(int staffId)
        {
            return _allStaff[staffId].MentorId;
        }

        public void SetMentorId(int staffId, int mentorId = 0)
        {
            RemoveSubordinate(_allStaff[staffId].MentorId, staffId);
            _allStaff[staffId].MentorId = mentorId;
            _repository.Update(_allStaff[staffId].ToStaffDAL());
            AddSubordinate(mentorId, staffId);
        }

        public void AddSubordinate(int mentorId, int subordinateId)
        {
            _allStaff[mentorId].AddSubordinate(subordinateId);
            _repository.Update(_allStaff[mentorId].ToStaffDAL());
        }

        public void RemoveSubordinate(int mentorId, int subordinateId)
        {
            _allStaff[mentorId].RemoveSubordinate(subordinateId);
            _repository.Update(_allStaff[mentorId].ToStaffDAL());
        }

        public IEnumerable<StaffDTO> GetAllStaff()
        {
            return _allStaff.Values;
        }
    }
}