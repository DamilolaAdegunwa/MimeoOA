using Abp.Dependency;
using Abp.Modules;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.DoNetCore
{
    public static class AbpServiceCollectionExtensions
    {

        public static IServiceProvider AddAbp<TSartModule>(this IServiceCollection service) where TSartModule:AbpModule
        {
            var abpBootstrapper = AddAbpBootstrapper<TSartModule>(service, IocManager.Instance);
            abpBootstrapper.IocManager.Builder.Populate(service);
            abpBootstrapper.IocManager.BuildComponent();
            return new AutofacServiceProvider(abpBootstrapper.IocManager.IocContainer);
        }
        private static AbpBootstrapper AddAbpBootstrapper<TStartModule>(IServiceCollection services, IIocManager iocManager) where TStartModule : AbpModule
        {
            var abpBootstrapper = AbpBootstrapper.Create<TStartModule>(iocManager);
            services.AddSingleton(abpBootstrapper);
            abpBootstrapper.Initialize();
            return abpBootstrapper;
        }
    }
}
