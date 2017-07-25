using Abp.AutoMapper;
using Abp.DoNetCore.Application;
using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.RedisCache;
using Autofac;

namespace Abp.DoNetCore
{
    [DependsOn(typeof(AbpEntityFrameworkCoreModule), typeof(AbpAutoMapperModule),typeof(AbpRedisCacheModule))]
    public class AbpDoNetCoreModule : AbpModule
    {
        public override void Initialize(ContainerBuilder builder)
        {
            Register<IUserAppService, UserAppService>(builder, Dependency.DependencyLifeStyle.Transient);
            Register<IAbpAuthorizationService, AbpAuthorizationService>(builder, Dependency.DependencyLifeStyle.Transient);
        }
    }
}
