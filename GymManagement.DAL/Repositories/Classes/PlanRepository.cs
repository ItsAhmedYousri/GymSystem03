using GymManagement.DAL.Data.DbContexts;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Repositories.Classes
{
    public class PlanRepository : GenericRepository<Plan>, IPlanRepository
    {
        private readonly GymDbContext _context;
        public PlanRepository(GymDbContext dbContext) : base(dbContext)
        {
            // _context = dbContext;
        }
        //public async Task<int> AddAsync(Plan p, CancellationToken ct = default)
        //{
        //    _context.Plans.Add(p);
        //    return await _context.SaveChangesAsync(ct);
        //}

        //public async Task<int> DeleteAsync(Plan p, CancellationToken ct = default)
        //{
        //   _context.Plans.Remove(p);
        //    return await _context.SaveChangesAsync(ct);
        //}

        //public Task<IEnumerable<Plan>> GetAllAsync(bool tracked = false, CancellationToken ct = default)
        //{
        //    throw new NotImplementedException();
        //}



        public Task<IEnumerable<Plan>> GetAllPlansWithDetailsAsync(bool tracked = false, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }


        //public Task<Plan> GetByIDAsync(int id, CancellationToken ct = default)
        //{
        //    throw new NotImplementedException();
        //}



        //public async Task<int> UpdateAsync(Plan p, CancellationToken ct = default)
        //{
        //    _context.Plans.Update(p);
        //    return await _context.SaveChangesAsync(ct);
        //}
    }
}
