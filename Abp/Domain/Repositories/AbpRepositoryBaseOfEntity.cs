using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abp.Domain.Repositories
{
  public abstract  class AbpRepositoryBaseOfEntity<TEntity>:IRepository<TEntity> where TEntity:class,IEntity<int>
    {
        public abstract IQueryable<TEntity> GetAll();

        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return GetAll();
        }

        public virtual List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsync()
        {
            return Task.FromResult(GetAllList());
        }

        public virtual List<TEntity> GetAllList(Func<TEntity, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsync(Func<TEntity, bool> predicate)
        {
            return Task.FromResult(GetAllList(predicate));
        }

        public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        public virtual TEntity Get(int id)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            var entity = await FirstOrDefaultAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public virtual TEntity Single(Func<TEntity, bool> predicate)
        {
            return GetAll().Single(predicate);
        }

        public virtual Task<TEntity> SingleAsync(Func<TEntity, bool> predicate)
        {
            return Task.FromResult(Single(predicate));
        }

        public virtual TEntity FirstOrDefault(int id)
        {
            return GetAll().FirstOrDefault(item => item.Id.Equals(id));
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(int id)
        {
            return Task.FromResult(FirstOrDefault(id));
        }

        public virtual TEntity FirstOrDefault(Func<TEntity, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Func<TEntity, bool> predicate)
        {
            return Task.FromResult(FirstOrDefault(predicate));
        }

        public virtual TEntity Load(int id)
        {
            return Get(id);
        }

        public abstract TEntity Insert(TEntity entity);

        public virtual Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public virtual int InsertAndGetId(TEntity entity)
        {
            return Insert(entity).Id;
        }

        public virtual Task<int> InsertAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertAndGetId(entity));
        }

        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            return entity.IsTransient()
                ? Insert(entity)
                : Update(entity);
        }

        public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return entity.IsTransient()
                ? await InsertAsync(entity)
                : await UpdateAsync(entity);
        }

        public virtual int InsertOrUpdateAndGetId(TEntity entity)
        {
            return InsertOrUpdate(entity).Id;
        }

        public virtual Task<int> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertOrUpdateAndGetId(entity));
        }

        public abstract TEntity Update(TEntity entity);

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        public virtual TEntity Update(int id, Action<TEntity> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(int id, Func<TEntity, Task> updateAction)
        {
            var entity = await GetAsync(id);
            await updateAction(entity);
            return entity;
        }

        public abstract void Delete(TEntity entity);

        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }

        public abstract void Delete(int id);

        public virtual Task DeleteAsync(int id)
        {
            Delete(id);
            return Task.FromResult(0);
        }

        public virtual void Delete(Func<TEntity, bool> predicate)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }

        public virtual Task DeleteAsync(Func<TEntity, bool> predicate)
        {
            Delete(predicate);
            return Task.FromResult(0);
        }

        public virtual int Count()
        {
            return GetAll().Count();
        }

        public virtual Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }

        public virtual int Count(Func<TEntity, bool> predicate)
        {
            return GetAll().Where(predicate).Count();
        }

        public virtual Task<int> CountAsync(Func<TEntity, bool> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        public virtual long LongCount()
        {
            return GetAll().LongCount();
        }

        public virtual Task<long> LongCountAsync()
        {
            return Task.FromResult(LongCount());
        }

        public virtual long LongCount(Func<TEntity, bool> predicate)
        {
            return GetAll().Where(predicate).LongCount();
        }

        public virtual Task<long> LongCountAsync(Func<TEntity, bool> predicate)
        {
            return Task.FromResult(LongCount(predicate));
        }

        public IQueryable<TEntity> GetAllIncluding(params Func<TEntity, object>[] propertySelectors)
        {
            return GetAll();
        }
    }
}
