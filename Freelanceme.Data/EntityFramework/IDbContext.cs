using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Freelanceme.Data.EntityFramework
{
    public interface IDbContext : IDbQueryable, IDisposable
    {
        Task<int> SaveAsync();

        void SaveSync();

        IDbContextTransaction BeginTransaction();
    }
}
