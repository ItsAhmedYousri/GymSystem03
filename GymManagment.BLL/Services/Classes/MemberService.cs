using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Repositories.Interfaces;
using GymManagment.BLL.Services.Interfaces;
using GymManagment.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagment.BLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> _memberRepository;
        public MemberService(IGenericRepository<Member> genericRepository)
        {
            _memberRepository = genericRepository;
        }

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default)
        {
            var emailExist= await _memberRepository.AnyAsync(m => m.Email == model.Email, ct);
            var phoneExist= await _memberRepository.AnyAsync(m => m.Phone == model.Phone, ct);

            if (emailExist || phoneExist) return false;

            var member = new Member
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Address = new Address()
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                },
                HealthRecord = new HealthRecord()
                {
                    Weight = model.HealthRecordViewModel.Weight,
                    Height = model.HealthRecordViewModel.Height,
                    Note = model.HealthRecordViewModel.Note,
                    BloodType = model.HealthRecordViewModel.BloodType
                }
            };

            var numberAffected = await _memberRepository.AddAsync(member);

            return numberAffected > 0;
        }

        public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken ct = default)
        {
            var members = await _memberRepository.GetAllAsync(ct:ct);

            if (!members.Any()) return [];

            var membersVeiwModel = members.Select(m => new MemberViewModel
            {
                Id = m.Id,
                Email = m.Email,
                Gender = m.Gender.ToString(),
                Name=m.Name,
                Phone=m.Phone,
                Photo=m.Photo??"?"

            });

            return membersVeiwModel;
        }
    }
}
