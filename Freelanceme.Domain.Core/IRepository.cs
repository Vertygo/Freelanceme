using Freelanceme.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Freelanceme.Domain.Core
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity entity);

        Task<TEntity> GetAsync(Guid id);

        Task<TEntity> GetAsync<TProperty>(Guid id, params Expression<Func<TEntity, TProperty>>[] paths);

        Task<List<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] paths);

        Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] paths);

        void Remove(TEntity entity);

        void Update(TEntity entity);

        Task<bool> SaveChangesAsync();

        void SaveChanges();
    }
}
