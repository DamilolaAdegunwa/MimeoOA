using Abp.DoNetCore;
using Abp.Modules;
using Autofac;
using Microsoft.EntityFrameworkCore;
using MimeoOAWeb.Core.Extensions;
using MimeoOAWeb.Core.MimeoDBContext;
using System;

namespace MimeoOAWeb.Core.Module
{
    [DependsOn(typeof(AbpDoNetCoreModule))]
    public class MimeoOAModule : AbpModule
    {
        public override void Initialize(ContainerBuilder builder)
        {
            //builder.AddDbContext<MimeoOAContext>((options, configuration) => {
            //    string connnectionsString = configuration["EntityFrameworkCore:MimeoOA:Connection"];
            //    Console.WriteLine(connnectionsString);
            //    options.UseMySQL(connnectionsString);
            //});

            //builder.Register(p => {
            //    var optionsBuilder = new DbContextOptionsBuilder<MimeoOAContext>();
            //    optionsBuilder.UseMySQL("Server=127.0.0.1;port=3306;database=mimeooa;uid=root;pwd=123456");
            //    return optionsBuilder.Options;
            //}).InstancePerLifetimeScope();

            builder.RegisterType<MimeoOAContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
        }
    }
}
