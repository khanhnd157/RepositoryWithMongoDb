
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MazeCore.MongoDb.Repository
{
    public interface IMongoRepository<TEntity> : IDisposable where TEntity : class
    {
        bool Any(Expression<Func<TEntity, bool>> condition = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition = null);

        TEntity GetFirst(Expression<Func<TEntity, bool>> condition);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> condition);

        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> condition);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> condition);

        IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> condition);

        Task<List<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> condition);

        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        TEntity Update(string id, TEntity entity);
        Task<TEntity> UpdateAsync(string id, TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

        void Delete(string id);
        Task DeleteAsync(string id);
    }
}
