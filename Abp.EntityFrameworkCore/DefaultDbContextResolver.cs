using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Abp.Dependency;
using Abp.EntityFrameworkCore.Configurations;

namespace Abp.EntityFrameworkCore
{
    public class DefaultDbContextResolver<TDbContext> : IDbContextResolver where TDbContext : DbContext
    {
        //private readonly AbpDbContext dbContext;

        //public DefaultDbContextResolver(AbpDbContext dbContext)
        //{
        //    //this.dbContext = serviceProvider.GetService(typeof(DbContext)) as DbContext;
        //    this.dbContext = dbContext;
        //}

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        //public DbContext Resolve(DBSelector dbSelector = DBSelector.Master)
        //{

        //    this.dbContext._dbSelector = dbSelector;
        //    return this.dbContext;
        //}

        private readonly Dictionary<DBSelector, DbContext> CacheDbContext = new Dictionary<DBSelector, DbContext>();
        private readonly IAbpDbContextConfigurer<TDbContext> dbContextConfigurer;
        public DefaultDbContextResolver(IAbpDbContextConfigurer<TDbContext> dbContextConfigurer)
        {
            //this.dbContext = serviceProvider.GetService(typeof(DbContext)) as DbContext;
            this.dbContextConfigurer = dbContextConfigurer;
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
            string connnectionString = "Server=127.0.0.1;port=3306;database=mimeooa;uid=root;pwd=123456";

            if (dbSelector == DBSelector.Master)
            {

            }
            else
            {

            }
            DbContext dbContext;
            CacheDbContext.TryGetValue(dbSelector, out dbContext);
            if (dbContext != null)
            {
                return dbContext;
            }
            var configurer = new AbpDbContextConfiguration<TDbContext>(connnectionString);
            dbContextConfigurer.Configure(configurer);
            var temp = typeof(TDbContext);
            dbContext = (DbContext)Activator.CreateInstance(temp, configurer.DbContextOptions.Options);
            CacheDbContext.Add(dbSelector, dbContext);
            return dbContext;
        }
    }
}
