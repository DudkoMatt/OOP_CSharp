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
        private IRepository<StaffDAL> _repository;
        
        public StaffService(IRepository<StaffDAL> repository)
        {
            _repository = repository;
        }
        
        public int CreateStaff(StaffDTO staffDTO)
        {
            var staffDAL = staffDTO.ToStaffDAL();
            _repository.Create(staffDAL);
            staffDTO.Id = staffDAL.Id;
            
            if (staffDTO.Id != 0)
                AddSubordinate(new StaffDTO(_repository.Get(0)), staffDTO);

            return staffDTO.Id;
        }

        public void FixStaffDTO(StaffDTO staffDTO)
        {
            var staffDAL = staffDTO.ToStaffDAL();
            _repository.Fix(staffDAL);
            staffDTO.Id = staffDAL.Id;
        }

        public int GetMentorId(StaffDTO staffDTO)
        {
            FixStaffDTO(staffDTO);
            return new StaffDTO(_repository.Get(staffDTO.Id)).MentorId;
        }

        public void SetMentor(StaffDTO staffDTO, StaffDTO mentorStaffDTO)
        {
            FixStaffDTO(staffDTO);
            FixStaffDTO(mentorStaffDTO);
            RemoveSubordinate(new StaffDTO(_repository.Get(staffDTO.MentorId)), staffDTO);
            staffDTO.MentorId = mentorStaffDTO.Id;
            _repository.Update(staffDTO.ToStaffDAL());
            AddSubordinate(mentorStaffDTO, staffDTO);
        }
        
        public void AddSubordinate(StaffDTO mentorStaffDto, StaffDTO subordinateStaffDTO)
        {
            FixStaffDTO(mentorStaffDto);
            FixStaffDTO(subordinateStaffDTO);
            mentorStaffDto.AddSubordinate(subordinateStaffDTO.Id);
            _repository.Update(mentorStaffDto.ToStaffDAL());
        }

        public void RemoveSubordinate(StaffDTO mentorStaffDTO, StaffDTO subordinateStaffDTO)
        {
            FixStaffDTO(mentorStaffDTO);
            FixStaffDTO(subordinateStaffDTO);
            mentorStaffDTO.RemoveSubordinate(subordinateStaffDTO.Id);
            _repository.Update(mentorStaffDTO.ToStaffDAL());
        }

        public IEnumerable<StaffDTO> GetAllStaff()
        {
            return _repository.GetAll().Select(t=> new StaffDTO(t));
        }

        public StaffDTO GetById(int staffId)
        {
            return new StaffDTO(_repository.Get(staffId));
        }
    }
}