using GymManagement.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GymManagement.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity>where TEntity : BaseEntity, new()
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool tracked = false, CancellationToken ct = default);
        Task<TEntity> GetByIDAsync(int id, CancellationToken ct = default);
        Task<int> AddAsync(TEntity entity, CancellationToken ct = default);
        Task<int> UpdateAsync(TEntity entity, CancellationToken ct = default);
        Task<int> DeleteAsync(TEntity entity, CancellationToken ct = default);
        Task<bool> AnyAsync(Expression<Func<TEntity,bool>>predicate, CancellationToken ct = default);
        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false,
                                                        CancellationToken ct = default);
    }
}
