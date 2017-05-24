using Abp.DoNetCore.Domain;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeoOAWeb.Core.Infrastructure;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimeoOAWeb.Core.MimeoDBContext
{
    public class MimeoOAContext : AbpDbContext
    {
        public DbSet<User> mo_user { get; set; }
        public DbSet<Role> mo_role { get; set; }
        public DbSet<UserInfo> mo_user_info { get; set; }
        public DbSet<UserRole> mo_user_role { get; set; }
        public DbSet<Permission> mo_permission { get; set; }
        public DbSet<RolePermission> mo_role_permission { get; set; }

        private readonly MimeoConfiguration mimeoConfiguration;
        public MimeoOAContext()
        {
            mimeoConfiguration = new MimeoConfiguration();
            ConfigurationManager.GetConfigSection("EntityFrameworkCore:MimeoConfiguration").Bind(mimeoConfiguration);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(mimeoConfiguration.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

    }
}
