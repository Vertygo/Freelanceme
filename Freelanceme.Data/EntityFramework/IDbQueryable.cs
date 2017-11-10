using Freelanceme.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Freelanceme.Data.EntityFramework
{
    public interface IDbQueryable
    {
        DbSet<TEntity> GetSet<TEntity>() where TEntity : Entity;
    }
}
