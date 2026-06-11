using GymManagement.DAL.Data.DbContexts;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GymManagement.DAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GymDbContext _context;
        private readonly DbSet<TEntity> _set;
        public GenericRepository(GymDbContext gymDbContext)
        {
            _context = gymDbContext;
            _set = _context.Set<TEntity>();

        }
        public async Task<int> AddAsync(TEntity entity, CancellationToken ct = default)
        {
            _set.Add(entity);
            return await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
        {
            return await _set.AsNoTracking().AnyAsync(predicate, ct);
        }

        public async Task<int> DeleteAsync(TEntity entity, CancellationToken ct = default)
        {
            _set.Remove(entity);
            return await _context.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracked = false, CancellationToken ct = default)
        {
            IQueryable<TEntity> query = tracked ? _set : _set.AsNoTracking();
            return await query.ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIDAsync(int id, CancellationToken ct = default)=>await _set.FindAsync(id,ct);
        

        public async Task<int> UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            _set.Update(entity);
            return await _context.SaveChangesAsync(ct);
        }
        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false,
                                                        CancellationToken ct = default)
        {
           IQueryable<TEntity> query=tracking? _set : _set.AsNoTracking();
            return await query.FirstOrDefaultAsync(predicate, ct);
        }
    }
}

