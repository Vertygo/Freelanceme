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

        TEntity Get(Guid id);

        IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] paths);

        void Remove(TEntity entity);

        void Modify(TEntity entity);

        Task<bool> SaveChangesAsync();

        void SaveChanges();
    }
}
