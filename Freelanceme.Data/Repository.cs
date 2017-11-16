using Freelanceme.Data.EntityFramework;
using Freelanceme.Domain.Common;
using Freelanceme.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Freelanceme.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly IDbContext _dbContext;

        public Repository(IDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public void Add(TEntity entity) => 
            _dbContext.GetSet<TEntity>().Add(entity);

        public async Task<TEntity> GetAsync(Guid id) => 
            await _dbContext.GetSet<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<List<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] paths)
        {
            var result = _dbContext.GetSet<TEntity>().Where(filter);

            if (!paths.Any())
                return await result.ToListAsync();

            foreach (var expression in paths)
            {
                result = result.Include(expression);
            }

            return await result.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] paths)
        {
            var result = _dbContext.GetSet<TEntity>().AsQueryable();

            if (!paths.Any())
                return await result.ToListAsync();

            foreach (var expression in paths)
            {
                result = result.Include(expression);
            }

            return await result.ToListAsync();
        }
            

        public void Remove(TEntity entity) =>        
            _dbContext.GetSet<TEntity>().Remove(entity);


        public void Modify(TEntity entity) =>
            _dbContext.GetSet<TEntity>().Update(entity);

        public async Task<bool> SaveChangesAsync() => 
            await _dbContext.SaveAsync() > 0;

        public void SaveChanges() =>
            _dbContext.SaveSync();
    }
}
