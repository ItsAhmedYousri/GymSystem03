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
        private readonly IGenericRepository<Membership> _membershipRepository;
        private readonly IGenericRepository<Plan> _planRepository;
        private readonly IGenericRepository<HealthRecord> _healthRecordRepository;
        private readonly IGenericRepository<Booking> _bookingRepository;
        public MemberService(IGenericRepository<Member> genericRepository, IGenericRepository<Plan> planRepository, IGenericRepository<Membership> membershipRepository, IGenericRepository<HealthRecord> healthRecordRepository, IGenericRepository<Booking> bookingRepository)
        {
            _memberRepository = genericRepository;
            _planRepository = planRepository;
            _membershipRepository = membershipRepository;
            _healthRecordRepository = healthRecordRepository;
            _bookingRepository = bookingRepository;
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

        public async Task<MemberViewModel?> GetMemberDetailsAsync(int id, CancellationToken ct = default)
        {
            var member=await _memberRepository.GetByIDAsync(id, ct);
            if(member == null) return null;

            var memberViewModel = new MemberViewModel()
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}"
            };

            var activeMembership = await _membershipRepository.FirstOrDefaultAsync(MP => MP.MemberId == member.Id
                && MP.EndDate > DateTime.Now, ct: ct);

            if (activeMembership is not null)
            {
                var activePlan = await _planRepository.GetByIDAsync(activeMembership.PlanId, ct);

                memberViewModel.PlanName = activePlan?.Name;
                memberViewModel.MembershipStartDate = activeMembership.CreatedAt.ToShortDateString();
                memberViewModel.MembershipEndDate = activeMembership.EndDate.ToShortDateString();
            }

            return memberViewModel;
        }

        public async Task<HealthRecordViewModel?> GetMemberHealthRecordDetailsAsync(int memberId, CancellationToken ct = default)
        {
            var record = await _healthRecordRepository.FirstOrDefaultAsync(h => h.MemberId == memberId, ct: ct);
            if (record == null) return null;

            else
            {
                return new HealthRecordViewModel()
                {
                    Weight = record.Weight,
                    BloodType=record.BloodType,
                    Height=record.Height,
                    Note = record.Note
                };

            }


            

        }
    }
}
