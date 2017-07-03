using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Abp.Dependency;
using Abp.EntityFrameworkCore.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Abp.EntityFrameworkCore
{
    public class DefaultDbContextResolver<TDbContext> : IDbContextResolver where TDbContext : DbContext
    {
        private readonly Dictionary<DBSelector, DbContext> CacheDbContext = new Dictionary<DBSelector, DbContext>();
        private readonly IAbpDbContextConfigurer<TDbContext> dbContextConfigurer;
        private readonly EFCoreDataBaseOptions dbOptions;
        public DefaultDbContextResolver(IAbpDbContextConfigurer<TDbContext> dbContextConfigurer,IOptions<EFCoreDataBaseOptions> dbOptions)
        {
            this.dbContextConfigurer = dbContextConfigurer;
            this.dbOptions = dbOptions.Value;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            foreach (var item in CacheDbContext)
            {
                item.Value.Dispose();
            }
            CacheDbContext.Clear();
        }
        public DbContext Resolve(DBSelector dbSelector = DBSelector.Master)
        {
            DbContext dbContext;
            CacheDbContext.TryGetValue(dbSelector, out dbContext);
            if (dbContext != null)
            {
                return dbContext;
            }
            var configurer = new AbpDbContextConfiguration<TDbContext>(dbOptions.DbConnections[dbSelector]);
            dbContextConfigurer.Configure(configurer);
            var actualContext = typeof(TDbContext);
            dbContext = (DbContext)Activator.CreateInstance(actualContext, configurer.DbContextOptions.Options);
            CacheDbContext.Add(dbSelector, dbContext);
            return dbContext;
        }
    }
}
