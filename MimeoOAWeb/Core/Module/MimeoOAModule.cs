using Abp.DoNetCore;
using Abp.Modules;
using Autofac;
using MimeoOAWeb.Core.Extensions;
using MimeoOAWeb.Core.MimeoDBContext;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System;

namespace MimeoOAWeb.Core.Module
{
    [DependsOn(typeof(AbpDoNetCoreModule))]
    public class MimeoOAModule : AbpModule
    {
        public override void Initialize(ContainerBuilder builder)
        {
            builder.AddDbContext<MimeoOAContext>((options, configuration) => {
                string connnectionsString = configuration["EntityFrameworkCore:MimeoOA:Connection"];
                Console.WriteLine(connnectionsString);
                options.UseMySQL(connnectionsString);
            });
        }
    }
}
