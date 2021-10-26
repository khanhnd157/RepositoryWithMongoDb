using MazeCore.MongoDb.Context;

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MazeCore.MongoDb.Repository
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoDatabase Database;
        protected readonly IMongoCollection<TEntity> DbSet;

        public MongoRepository(IMongoContext context)
        {
            Database = context.Database;
            DbSet = Database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> condition = null)
        {
            return DbSet.Find(condition).Any();
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition = null)
        {
            return DbSet.Find(condition).AnyAsync();
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> condition)
        {
            return DbSet.Find(condition).FirstOrDefault();
        }

        public virtual Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> condition)
        {
            return DbSet.Find(condition).FirstOrDefaultAsync();
        }

        public virtual TEntity GetFirst(Expression<Func<TEntity, bool>> condition)
        {
            return DbSet.Find(condition).First();
        }

        public virtual Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> condition)
        {
            return DbSet.Find(condition).FirstAsync();
        }

        public virtual IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> condition)
        {
            return DbSet.Find(condition).ToEnumerable();
        }

        public virtual Task<List<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> condition)
        {
            return DbSet.Find(condition).ToListAsync();
        }

        public virtual TEntity Add(TEntity entity)
        {
            try
            {
                DbSet.InsertOne(entity);
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                await DbSet.InsertOneAsync(entity);
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                DbSet.InsertMany(entities);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                return DbSet.InsertManyAsync(entities);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual TEntity Update(string id, TEntity entity)
        {
            try
            {
                DbSet.ReplaceOne(FilterId(id), entity);
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<TEntity> UpdateAsync(string id, TEntity entity)
        {
            try
            {
                await DbSet.ReplaceOneAsync(FilterId(id), entity);
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                try
                {
                    var id = entity.GetType().GetProperty("Id").GetValue(entity).ToString();
                    Update(id, entity);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public virtual Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            List<Task> tasks = new List<Task>();
            foreach (var entity in entities)
            {
                var id = entity.GetType().GetProperty("Id").GetValue(entity).ToString();
                tasks.Add(UpdateAsync(id, entity));
            }

            return Task.WhenAll(tasks);
        }

        public virtual void Delete(string id)
        {
            try
            {
                DbSet.DeleteOne(FilterId(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual Task DeleteAsync(string id)
        {
            try
            {
                return DbSet.DeleteOneAsync(FilterId(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private static FilterDefinition<TEntity> FilterId(string key)
        {
            return Builders<TEntity>.Filter.Eq("Id", key);
        }

        private string GetCollectionName(Type type)
        {
            Attribute[] attrs = System.Attribute.GetCustomAttributes(type);

            foreach (var attr in attrs)
            {
                if (attr is BsonDiscriminatorAttribute)
                {
                    var discriminator = (BsonDiscriminatorAttribute)attr;
                    return discriminator.Discriminator;
                }
            }

            return nameof(type);
        }
    }
}
