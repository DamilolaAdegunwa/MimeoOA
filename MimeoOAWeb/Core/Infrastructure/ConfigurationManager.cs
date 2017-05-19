using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimeoOAWeb.Core.Infrastructure
{
    /// <summary>
    /// get configration through appsettings.json
    /// </summary>
    public class ConfigurationManager
    {

        private static IConfiguration config;

        public static void SetConfig(IConfiguration cig)
        {
            config = cig;
        }

        public static string GetConfigValue(string name)
        {

            if (config.GetSection(name) == null)
                return "";

            return config.GetSection(name).Value;
        }

        public static IConfigurationSection GetConfigSection(string name)
        {
            var sectionName= config.GetSection(name);
            return sectionName;
        }

    }
}
