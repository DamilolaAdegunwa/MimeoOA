using Abp.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Abp.DoNetCore.Application;
using Abp.EntityFrameworkCore;
using Abp.AutoMapper;

namespace Abp.DoNetCore
{
    [DependsOn(typeof(AbpEntityFrameworkCoreModule), typeof(AbpAutoMapperModule))]
    public class AbpDoNetCoreModule : AbpModule
    {
        public override void Initialize(ContainerBuilder builder)
        {
            //builder.RegisterType<UserAppService>().As<IUserAppService>().InstancePerLifetimeScope();
            Register<IUserAppService, UserAppService>(builder, Dependency.DependencyLifeStyle.Transient);
        }
    }
}
