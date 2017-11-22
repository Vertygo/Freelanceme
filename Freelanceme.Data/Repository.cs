using Freelanceme.Data.EntityFramework;
using Freelanceme.Domain.Common;
using Freelanceme.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Freelanceme.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly IDbContext _dbContext;

        public Repository(IDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public void Add(TEntity entity) =>
            _dbContext.GetSet<TEntity>().Add(entity);

        public void Update(TEntity entity) =>
            _dbContext.GetSet<TEntity>().Update(entity);

        public Task<TEntity> GetAsync(Guid id) =>
            _dbContext.GetSet<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));

        public Task<TEntity> GetAsync<TProperty>(Guid id, params Expression<Func<TEntity, TProperty>>[] paths) =>
            GetAsync<TEntity>(f => f.Id == id);

        public Task<TEntity> GetAsync<TProperty>(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, TProperty>>[] paths)
        {
            var result = _dbContext.GetSet<TEntity>().Where(filter);

            if (paths.Length == 0)
                return result.FirstOrDefaultAsync();

            IIncludableQueryable<TEntity, TProperty> includeResult = null;

            foreach (var expression in paths)
                includeResult = result.Include(expression);

            return includeResult.FirstOrDefaultAsync();
        }

        public Task<List<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] paths)
        {
            var result = _dbContext.GetSet<TEntity>().Where(filter);

            if (paths.Length == 0)
                return result.ToListAsync();

            foreach (var expression in paths)
                result = result.Include(expression);

            return result.ToListAsync();
        }

        public Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] paths)
        {
            var result = _dbContext.GetSet<TEntity>().AsQueryable();

            if (paths.Length == 0)
                return result.ToListAsync();

            foreach (var expression in paths)
                result = result.Include(expression);

            return result.ToListAsync();
        }
            

        public void Remove(TEntity entity) =>
            _dbContext.GetSet<TEntity>().Remove(entity);

        public async Task<bool> SaveChangesAsync() =>
            await _dbContext.SaveAsync() > 0;

        public void SaveChanges() =>
            _dbContext.SaveSync();

    }
}
