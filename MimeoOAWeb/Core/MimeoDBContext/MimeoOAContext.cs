using Abp.DoNetCore.Domain;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimeoOAWeb.Core.MimeoDBContext
{
    public class MimeoOAContext : AbpDbContext
    {
        public DbSet<User> Users { get; set; }
        public MimeoOAContext(DbContextOptions options) : base(options)
        {
        }
    }
}
