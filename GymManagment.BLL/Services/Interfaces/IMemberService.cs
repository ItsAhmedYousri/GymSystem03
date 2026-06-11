using GymManagment.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagment.BLL.Services.Interfaces
{
    public interface IMemberService
    {
        public Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken ct = default);
        public Task<bool> CreateMemberAsync(CreateMemberViewModel memberViewModel, CancellationToken ct = default);

        public Task<MemberViewModel> GetMemberDetailsAsync(int id, CancellationToken ct = default);

        public Task<HealthRecordViewModel> GetMemberHealthRecordDetailsAsync(int memberId, CancellationToken ct = default);
    }
}
