using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Web.Http
{
    public static class HttpConfigurationExtension
    {
        public static void UseRazor(this HttpConfiguration config)
        {
            TemplateServiceConfiguration templateConfig = new TemplateServiceConfiguration();
            templateConfig.Debug = true;
            Engine.Razor = RazorEngineService.Create(templateConfig);
            TryGetRootPath(config);
            var rootPath = config.RootPath();
            var cshtmls = Directory.GetFiles(rootPath, "*.cshtml", SearchOption.AllDirectories);
            foreach (var item in cshtmls)
            {
                var name = item.Replace(rootPath, "~").Replace("\\","/");
                Engine.Razor.AddTemplate(name, File.ReadAllText(item));
            }
        }

        private static void TryGetRootPath(HttpConfiguration config)
        {
            if (!config.Properties.ContainsKey("RootPath"))
            {
                string rootPath = null;
                var assembly = Assembly.GetEntryAssembly();
                if (assembly != null)
                {
                    rootPath = Path.GetDirectoryName(assembly.Location);
                }
                if (rootPath == null)
                {
                    assembly = Assembly.GetCallingAssembly();
                    rootPath = Path.GetDirectoryName(new Uri(assembly.CodeBase).LocalPath);
                }
                if (rootPath == null)
                {
                    throw new Exception();
                }
                if (!config.Properties.TryAdd("RootPath", rootPath))
                {
                    throw new Exception();
                }
            }
        }

        public static string RootPath(this HttpConfiguration config)
        {
            return (string)config.Properties["RootPath"];
        }
    }
}
