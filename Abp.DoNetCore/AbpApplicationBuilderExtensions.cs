using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.DoNetCore
{
    public static class AbpApplicationBuilderExtensions
    {
        public static void UserAbp(this IApplicationBuilder app)
        {
            InitializeAbp(app);
        }
        private static void InitializeAbp(IApplicationBuilder app)
        {
            var abpBootstrapper = app.ApplicationServices.GetRequiredService<AbpBootstrapper>();
            abpBootstrapper.Initialize();
        }
    }
}
