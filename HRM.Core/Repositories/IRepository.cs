using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Core.Entities;

namespace Terp.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        IQueryable<TEntity> AsQueryable();
    }
}
