using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Models.Abstractions;

namespace Core.Interfaces
{
    public interface IRepository<TEntity, in TId> where TEntity : Entity<TId> where TId : struct
    {
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

        void Delete(TId id);
        Task DeleteAsync(TId id, CancellationToken cancellationToken);

        bool Exists(TId id);
        Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        TEntity GetById(TId id);
        Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken);

        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    }
}