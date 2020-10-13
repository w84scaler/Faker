using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;

namespace FakerLib
{
    class PluginLoader
    {
        private readonly string pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");

        public List<IGenerator> RefreshPlugins()
        {
            List<IGenerator> PluginList = new List<IGenerator>();

            DirectoryInfo PluginDirectory = new DirectoryInfo(pluginPath);
            if (!PluginDirectory.Exists) PluginDirectory.Create();

            var PluginFiles = Directory.GetFiles(pluginPath, "*.dll");
            foreach (var file in PluginFiles)
            {
                Assembly asm = Assembly.LoadFrom(file);
                var types = asm.GetTypes().
                                Where(t => t.GetInterfaces().
                                Where(i => i.FullName == typeof(IGenerator).FullName).Any());

                foreach (var type in types)
                {
                    var plugin = asm.CreateInstance(type.FullName) as IGenerator;
                    PluginList.Add(plugin);
                }
            }
            return PluginList;
        }
    }
}
