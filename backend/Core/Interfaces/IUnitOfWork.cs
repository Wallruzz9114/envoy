using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
        void Commit();
        void Commit(IDbContextTransaction transaction);
        Task CommitAsync(CancellationToken cancellationToken);
        Task CommitAsync(IDbContextTransaction transaction, CancellationToken cancellationToken);
        void Rollback();
        void Rollback(IDbContextTransaction transaction);
        Task RollbackAsync(CancellationToken cancellationToken);
        void RollbackAsync(IDbContextTransaction transaction, CancellationToken cancellationToken);
        void SaveChanges();
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}