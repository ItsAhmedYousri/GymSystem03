using GymManagement.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Repositories.Interfaces
{
    public interface IPlanRepository:IGenericRepository<Plan>
    {
        //Task<IEnumerable<Plan>> GetAllPlansAsync(bool tracked=false,CancellationToken ct=default);
        //Task<Plan> GetPlanByIDAsync(int id, CancellationToken ct = default);
        //Task<int> AddAsync(Plan p, CancellationToken ct = default);
        //Task<int> UpdateAsync(Plan p, CancellationToken ct = default);
        //Task<int> DeleteAsync(Plan p, CancellationToken ct = default);

        Task<IEnumerable<Plan>> GetAllPlansWithDetailsAsync(bool tracked = false, CancellationToken ct = default);
    }
}
