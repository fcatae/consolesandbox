using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginSandbox
{
    public class Sandbox
    {
        string _pluginDirectory;

        public Sandbox()
        {
            _pluginDirectory = ".";
        }

        public Sandbox(string pluginDirectory)
        {
            _pluginDirectory = pluginDirectory;
        }
        
        public IPlugin CreateLocal<T>() where T: class
        {
            Type t = typeof(T);
            string assemblyName = t.Assembly.FullName;
            string typeName = t.FullName;

            var assembly = Assembly.Load(assemblyName);

            var obj = assembly.CreateInstance(typeName);

            return CreateProxy(obj);
        }
        public IPlugin Create<T>() where T : IPlugin, new()
        {
            //Type t = typeof(T);
            Type t = typeof(PluginFactory<T>);
            
            string assemblyName = t.Assembly.FullName;
            string typeName = t.FullName;

            var appDomain = CreateAppDomain(typeName, _pluginDirectory);
            var remoteFactory = (PluginFactory<T>)appDomain.CreateInstanceAndUnwrap(assemblyName, typeName);

            return remoteFactory.Create();
        }

        AppDomain CreateAppDomain(string name, string path)
        {
            var appInfo = new AppDomainSetup() { ApplicationBase = Path.GetFullPath(path) };
            var appDomain = AppDomain.CreateDomain(name, null, appInfo);

            appDomain.AssemblyResolve += SandboxAssemblyResolver;

            return appDomain;
        }

        private static Assembly SandboxAssemblyResolver(object sender, ResolveEventArgs args)
        {
            AppDomain domain = (AppDomain)sender;
            string baseDirectory = domain.BaseDirectory;

            string dll = FindAssemblyFile(baseDirectory, args.Name);

            var asm = Assembly.LoadFile(dll);

            return asm;
        }

        private static string FindAssemblyFile(string baseDirectory, string assemblyName)
        {
            AssemblyName asmName = new AssemblyName(assemblyName);
            string name = (new AssemblyName(assemblyName)).Name;
            string defaultName = $"{name}.dll";

            string[] validPaths = new string[] {
                // Path.Combine(baseDirectory, "Plugin", this._plugin, $"{name}.dll"),
                Path.Combine(baseDirectory, "Plugin", $"{name}.dll")
            };

            foreach (string path in validPaths)
            {
                if (File.Exists(path))
                    return path;
            }

            return defaultName;
        }

        IPlugin CreateProxy(object obj)
        {
            return (IPlugin)obj;
        }
    }
}
