﻿using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.EntityFrameworkCore.Repositories
{
    public class EfCoreRepositoryBaseOfEntity<EFEntity> : AbpRepositoryBaseOfEntity<EFEntity>, IRepository<EFEntity>
        where EFEntity : class, IEntity<Guid>
    {
        public virtual DbContext Context { get { return _dbContextProvider.Resolve(); } }
        public virtual DbSet<EFEntity> Table { get { return Context.Set<EFEntity>(); } }
        private readonly IDbContextResolver _dbContextProvider;
        public EfCoreRepositoryBaseOfEntity(IDbContextResolver dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public override IQueryable<EFEntity> GetAll()
        {
            return Table;
        }

        public override EFEntity Insert(EFEntity entity)
        {
            return Table.Add(entity).Entity;
        }

        public override EFEntity Update(EFEntity entity)
        {
            return Table.Update(entity).Entity;
        }

        public override void Delete(EFEntity entity)
        {
            Table.Remove(entity);
        }

        public override void Delete(Guid id)
        {
            var removeEntity = Get(id);
            if (removeEntity != null)
            {
                Table.Remove(removeEntity);
            }
        }
    }
}
