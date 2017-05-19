using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.EntityFrameworkCore.Repositories
{
    public class EfCoreRepositoryBaseOfEntity<TEntity> : AbpRepositoryBaseOfEntity<TEntity>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        public virtual DbContext Context { get { return _dbContextProvider.Resolve(); } }
        public virtual DbSet<TEntity> Table { get { return Context.Set<TEntity>(); } }
        private readonly IDbContextResolver _dbContextProvider;
        public EfCoreRepositoryBaseOfEntity(IDbContextResolver dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Table.Where(item => item.Id > 0);
        }

        public override TEntity Insert(TEntity entity)
        {
            return Table.Add(entity).Entity;
        }

        public override TEntity Update(TEntity entity)
        {
            return Table.Update(entity).Entity;
        }

        public override void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public override void Delete(int id)
        {
            var removeEntity = Get(id);
            if (removeEntity != null)
            {
                Table.Remove(removeEntity);
            }
        }
    }
}
