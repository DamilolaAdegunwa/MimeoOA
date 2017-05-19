using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Abp.EntityFrameworkCore.Repositories
{
    public class EfCoreRepositoryBase<TEntity> : AbpRepositoryBase<TEntity, int> where TEntity : class, IEntity<int>
    {
        public virtual DbContext Context { get { return _dbContextProvider.Resolve(); } }
        public virtual DbSet<TEntity> Table { get { return Context.Set<TEntity>(); } }
        private readonly IDbContextResolver _dbContextProvider;
        public EfCoreRepositoryBase(IDbContextResolver dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        public override void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public override void Delete(int id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if(entity!=null)
            {
                Delete(entity);
                return;
            }
            entity = FirstOrDefault(id);
            if (entity!=null)
            {
                Delete(entity);
                return;
            }
        }

        public override IQueryable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public override TEntity Insert(TEntity entity)
        {
            Table.Add(entity);
            return entity;
        }

        public override TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        private TEntity GetFromChangeTrackerOrNull(int id)
        {
            var entry = Context.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<int>.Default.Equals(id, (ent.Entity as TEntity).Id)
                );

            return entry?.Entity as TEntity;
        }
    }
}
