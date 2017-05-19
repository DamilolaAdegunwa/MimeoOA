using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Abp.Dependency;

namespace Abp.EntityFrameworkCore
{
    public class DefaultDbContextResolver : IDbContextResolver
    {
        private readonly DbContext dbContext;

        public DefaultDbContextResolver(DbContext dbContext)
        {
            //this.dbContext = serviceProvider.GetService(typeof(DbContext)) as DbContext;
            this.dbContext = dbContext;
        }
        public DbContext Resolve()
        {
            return this.dbContext;
        }
    }
}
