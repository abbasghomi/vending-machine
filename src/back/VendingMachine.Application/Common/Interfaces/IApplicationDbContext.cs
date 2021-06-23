using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace VendingMachine.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {

        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken());

    }
}
